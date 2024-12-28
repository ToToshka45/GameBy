using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatingService.Application.Services;
using RatingService.Domain.Abstractions;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;
using RatingService.Domain.Entities.Ratings;
using RatingService.Domain.Enums;
using RatingService.Infrastructure.DataAccess;

namespace RatingService.Infrastructure.Repositories;

public class RatingsRepository(RatingServiceDbContext storage, InMemoryCachingService cacheService, ILogger<RatingsRepository> logger, IRepository<UserInfo> userInfoRepo)
    : IRatingsRepository
{
    private readonly DbSet<ParticipantRating> _participantRatings = storage.Set<ParticipantRating>();
    private readonly DbSet<EventRating> _eventRatings = storage.Set<EventRating>();
    private readonly DbSet<UserInfo> _usersInfo = storage.Set<UserInfo>();
    private readonly DbSet<EventInfo> _eventsInfo = storage.Set<EventInfo>();
    private readonly DbSet<Participant> _participants = storage.Set<Participant>();
    private readonly InMemoryCachingService _cacheService = cacheService;

    private readonly ILogger<RatingsRepository> _logger = logger;

    public async Task AddOrUpdate(RatingUpdate update, CancellationToken token)
    {
        using var tran = await storage.Database.BeginTransactionAsync(token);
        if (update is ParticipantRatingUpdate)
        {
            // add a new rating update
            var rating = await _participantRatings.Include(e => e.Updates).FirstOrDefaultAsync(e => e.ExternalParticipantId == update.SubjectId);
            if (rating == null) { return; }
            update.SetRatingRelation(rating.Id);
            rating.AddRatingUpdate(update);
            await storage.SaveChangesAsync();

            await Recalculate((ParticipantRatingUpdate)update, token);
        }
        else if (update is EventRatingUpdate)
        {
            // add a new rating update
            var rating = await _eventRatings.Include(e => e.Updates).FirstOrDefaultAsync(e => e.ExternalEventId == update.SubjectId);
            if (rating == null) { return; }
            update.SetRatingRelation(rating.Id);
            rating.AddRatingUpdate(update);
            await storage.SaveChangesAsync();

            await Recalculate((EventRatingUpdate)update, token);
        }

        await tran.CommitAsync(token);
    }

    private async Task Recalculate(ParticipantRatingUpdate update, CancellationToken token)
    {
        var subjectId = update.SubjectId;

        // Stage 1
        // there should be no problems with dividing by 0, since this method is gonna be called when at least 1 Update has already been stored
        float newValue = await _participantRatings
            .Include(e => e.Updates)
            .Where(e => e.ExternalParticipantId == subjectId)
            .Select(e => new { Sum = e.Updates.Sum(x => x.Value), Count = e.Updates.Count() })
            .Select(e => e.Sum / e.Count)
            .FirstOrDefaultAsync(token);

        // try to get a rating from the cache, if it`s empty - get it from db and store in the cache
        var rating = await GetRating(subjectId, EntityType.Participant, token);
        ArgumentNullException.ThrowIfNull(rating);

        rating.SetUpdatedValue(newValue);
        //_ratings.Update(rating);

        await storage.SaveChangesAsync(token);

        // set cache
        _cacheService.SetRatingValue(rating.Id, EntityType.Participant, rating);

        // Stage 2: Recalculate a base rating

        // get a participant, that stores a userId value
        var userId = await _participants
            .Where(e => e.ExternalParticipantId == subjectId)
            .Select(e => e.ExternalUserId)
            .FirstOrDefaultAsync(token);

        // calculate a new rating value
        var obj = await _usersInfo
            //.Include(e => e.GamerRating)
            //.ThenInclude(e => e.ParticipantRatings)
            .Where(e => e.ExternalUserId == userId)
            .Select(e => new
            {
                e.GamerRating,
                Sum = e.GamerRating.ParticipantRatings.Sum(x => x.Value),
                Count = e.GamerRating.ParticipantRatings.Count()
            })
            .Select(e => new { e.GamerRating, newValue = e.Sum / e.Count })
            .FirstOrDefaultAsync(token);
        ArgumentNullException.ThrowIfNull(obj);

        obj.GamerRating.SetUpdatedValue(obj.newValue);
        await storage.SaveChangesAsync(token);

        _cacheService.SetRatingValue(obj.GamerRating.Id, EntityType.Gamer, obj.GamerRating);
    }

    private async Task Recalculate(EventRatingUpdate update, CancellationToken token)
    {
        var subjectId = update.SubjectId;

        // Stage 1

        // there should be no problems with dividing by 0, since this method is gonna be called when at least 1 Update has already been stored
        float newValue = await _eventRatings
            .Include(e => e.Updates)
            .Where(e => e.ExternalEventId == subjectId)
            .Select(e => new { Sum = e.Updates.Sum(x => x.Value), Count = e.Updates.Count() })
            .Select(e => e.Sum / e.Count)
            .FirstOrDefaultAsync(token);

        // try to get a rating from the cache, if it`s empty - get it from db and store in the cache
        var rating = await GetRating(subjectId, EntityType.Participant, token);
        ArgumentNullException.ThrowIfNull(rating);

        rating.SetUpdatedValue(newValue);
        //_ratings.Update(rating);

        await storage.SaveChangesAsync(token);

        // set cache
        _cacheService.SetRatingValue(rating.Id, EntityType.Event, rating);

        // Stage 2: Recalculate a base rating

        // an organizerId of an event is the ExternalUserId in the UserInfo table
        var organizerId = await _eventsInfo
            .Where(e => e.ExternalEventId == subjectId)
            .Select(e => e.OrganizerId)
            .FirstOrDefaultAsync(token);

        // calculate a new rating value
        var obj = await _usersInfo
            //.Include(e => e.OrganizerRating)
            //.ThenInclude(e => e.EventRatings)
            .Where(e => e.ExternalUserId == organizerId)
            .Select(e => new
            {
                e.OrganizerRating,
                Sum = e.GamerRating.ParticipantRatings.Sum(x => x.Value),
                Count = e.GamerRating.ParticipantRatings.Count()
            })
            .Select(e => new { e.OrganizerRating, newValue = e.Sum / e.Count })
            .FirstOrDefaultAsync(token);
        ArgumentNullException.ThrowIfNull(obj);

        obj.OrganizerRating.SetUpdatedValue(obj.newValue);
        await storage.SaveChangesAsync(token);

        _cacheService.SetRatingValue(obj.OrganizerRating.Id, EntityType.Organizer, obj.OrganizerRating);
    }

    private async ValueTask<RatingBase?> GetRating(int ratingId, EntityType type, CancellationToken token)
    {
        if (_cacheService.TryRetrieve(ratingId, type, out var rating))
        {
            _logger.LogTrace($"Retreived the Rating with the Value of '{rating!.Value}'.");
            return rating;
        }
        _logger.LogInformation("Retreived data was null. Trying to get from Database.");

        RatingBase? storedRating = null;
        if (type is EntityType.Participant)
        {
            storedRating = await _participantRatings.FirstOrDefaultAsync(e => e.Id == ratingId, token);
        }
        else if (type is EntityType.Event)
        {
            storedRating = await _eventRatings.FirstOrDefaultAsync(e => e.Id == ratingId, token);
        }

        if (storedRating != null)
        {
            _logger.LogTrace($"Received data from the Database. Adding to the Cache storage.");
            _cacheService.SetRatingValue(ratingId, type, storedRating);
            return storedRating;
        }
        _logger.LogTrace($"No data has been found for the Subject with Id '{ratingId}'.");
        return null;
    }
}
