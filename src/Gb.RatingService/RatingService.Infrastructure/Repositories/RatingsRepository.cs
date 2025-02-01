using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatingService.Domain.Abstractions;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;
using RatingService.Domain.Entities.Ratings;
using RatingService.Common.Enums;
using RatingService.Infrastructure.DataAccess;
using RatingService.Application.Services.Caching;

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
        if (update is ParticipantRatingUpdate participantRatingUpdate)
        {
            var rating = await _participantRatings.Include(e => e.Updates)
                .FirstOrDefaultAsync(e => e.Id == participantRatingUpdate.SubjectId);

            if (rating == null) { return; }
            participantRatingUpdate.SetRatingRelation(rating.Id);
            rating.AddRatingUpdate(participantRatingUpdate);
            await storage.SaveChangesAsync();

            await Recalculate((ParticipantRatingUpdate)update, token);
        }
        else if (update is EventRatingUpdate eventRatingUpdate)
        {
            var rating = await _eventRatings.Include(e => e.Updates)
                .FirstOrDefaultAsync(e => e.Id == eventRatingUpdate.SubjectId);

            if (rating == null) { return; }
            eventRatingUpdate.SetRatingRelation(rating.Id);
            rating.AddRatingUpdate(eventRatingUpdate);
            await storage.SaveChangesAsync();

            await Recalculate(eventRatingUpdate, token);
        }

        await tran.CommitAsync(token);
    }

    private async Task Recalculate(ParticipantRatingUpdate update, CancellationToken token)
    {
        var subjectId = update.SubjectId;

        // Stage 1
        // there should be no problems with dividing by 0, since this method is gonna be called when at least 1 Update has already been stored
        var data = await _participantRatings
            .Include(e => e.Updates)
            .Where(e => e.Id == subjectId)
            .Select(e => new { Sum = e.Updates.Sum(x => x.Value), Count = e.Updates.Count() })
            .FirstOrDefaultAsync(token);
        ArgumentNullException.ThrowIfNull(data);

        var newValue = (float)Math.Round(data.Sum / data.Count, 2);

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
        var userInfoId = await _participants
            .Where(e => e.Id == subjectId)
            .Select(e => e.UserInfoId)
            .FirstOrDefaultAsync(token);

        // calculate a new rating value
        var obj = await _usersInfo
            .Include(e => e.GamerRating)
            .ThenInclude(e => e!.ParticipantRatings)
            .Where(e => e.Id == userInfoId)
            .Select(e => new
            {
                e.GamerRating,
                Sum = e.GamerRating!.ParticipantRatings.Sum(x => x.Value),
                Count = e.GamerRating.ParticipantRatings.Count()
            })
            .FirstOrDefaultAsync(token);

        ArgumentNullException.ThrowIfNull(obj);

        newValue = (float)Math.Round(obj.Sum / obj.Count, 2);

        obj.GamerRating!.SetUpdatedValue(newValue);
        await storage.SaveChangesAsync(token);

        _cacheService.SetRatingValue(obj.GamerRating.Id, EntityType.Gamer, obj.GamerRating);
    }

    private async Task Recalculate(EventRatingUpdate update, CancellationToken token)
    {
        var subjectId = update.SubjectId;

        // Stage 1

        // TODO: understand, why do I get the numeric value overflow error here, an error comes from Npgsql side,
        // so it`s something related to how a value is being stored on PostgreSQL side...

        //float newValue = (float)await _eventRatings
        //    .Include(e => e.Updates)
        //    .Where(e => e.Id == subjectId)
        //    .Select(e => new { Sum = e.Updates.Sum(x => x.Value), Count = e.Updates.Count() })
        //    .Select(e => Math.Round(e.Sum / e.Count, 2))
        //    .FirstOrDefaultAsync(token);

        var data = await _eventRatings
            .Include(e => e.Updates)
            .Where(e => e.Id == subjectId)
            .Select(e => new { Sum = e.Updates.Sum(x => x.Value), Count = e.Updates.Count() }).FirstOrDefaultAsync();
        ArgumentNullException.ThrowIfNull(data);

        // since we get an error during this part of calculating, we`ll do in on the application side
        var newValue = (float)Math.Round(data.Sum / data.Count, 2);

        // try to get a rating from the cache, if it`s empty - get it from db and store in the cache
        var rating = await GetRating(subjectId, EntityType.Event, token);
        ArgumentNullException.ThrowIfNull(rating);

        rating.SetUpdatedValue((float)newValue);
        //_ratings.Update(rating);

        await storage.SaveChangesAsync(token);

        // set cache
        _cacheService.SetRatingValue(rating.Id, EntityType.Event, rating);

        // Stage 2: Recalculate a base rating

        // an organizerId of an event is the ExternalUserId in the UserInfo table
        var organizerId = await _eventsInfo
            .Where(e => e.Id == subjectId)
            .Select(e => e.OrganizerId)
            .FirstOrDefaultAsync(token);

        // calculate a new rating value
        var obj = await _usersInfo
            .Include(e => e.OrganizerRating)
            .ThenInclude(e => e.EventRatings)
            .Where(e => e.Id == organizerId)
            .Select(e => new
            {
                e.OrganizerRating,
                Sum = e.OrganizerRating!.EventRatings.Sum(x => x.Value),
                Count = e.OrganizerRating.EventRatings.Count()
            }).FirstOrDefaultAsync(token);
        ArgumentNullException.ThrowIfNull(obj);

        // the same as the rework above we now make the calculation on the application side
        newValue = (float)Math.Round(obj.Sum / obj.Count, 2);

        ArgumentNullException.ThrowIfNull(obj);
        ArgumentNullException.ThrowIfNull(obj.OrganizerRating);

        obj.OrganizerRating.SetUpdatedValue(newValue);
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
