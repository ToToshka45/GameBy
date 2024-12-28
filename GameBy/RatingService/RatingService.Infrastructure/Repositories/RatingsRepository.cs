using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatingService.Application.Services;
using RatingService.Domain.Abstractions;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;
using RatingService.Domain.Enums;
using RatingService.Infrastructure.DataAccess;

namespace RatingService.Infrastructure.Repositories;

public class RatingsRepository(RatingServiceDbContext storage, InMemoryCachingService cacheService, ILogger<RatingsRepository> logger, IRepository<UserInfo> userInfoRepo) 
    : IRatingsRepository
{
    private readonly DbSet<Rating> _ratings = storage.Set<Rating>();
    private readonly IRepository<UserInfo> _userInfoRepo = userInfoRepo;
    private readonly InMemoryCachingService _cacheService = cacheService;

    private readonly ILogger<RatingsRepository> _logger = logger;

    public async Task AddOrUpdate(RatingUpdate update, CancellationToken token)
    {
        if (update.EntityType is EntityType.Participant)
        {
            // add a new rating update
            var rating = await _ratings.Include(e => e.Updates).FirstOrDefaultAsync(e => e.SubjectId == update.SubjectId);
            if (rating == null) { return; }
            rating.AddRatingUpdate(update);
            await storage.SaveChangesAsync();

            // recalculate an average participant rating value
            // level of isolation is Read Commited by default in PostgreSQL, should be sufficient
            using var tran = await storage.Database.BeginTransactionAsync(token);

            _ = Recalculate(update.SubjectId, update.EntityType, token);

            await tran.CommitAsync(token);

            // set cache
            _cacheService.SetRatingValue(update.RatingId, rating);

            // recalculate the gamer rating value
            var userInfo = await _userInfoRepo.GetEntityWithIncludesAsync(update.EventId, token, [e => e!.GamerRating.Updates]);
            ArgumentNullException.ThrowIfNull(userInfo);

            userInfo.GamerRating.Recalculate();
            await storage.SaveChangesAsync();
        }

    }

    public async ValueTask<Rating?> GetRating(int subjectId, CancellationToken token)
    {
        if (_cacheService.TryRetrieve(subjectId, out var rating))
        {
            _logger.LogTrace($"Retreived the Rating with the Value of '{rating!.Value}'.");
            return rating;
        }
        _logger.LogInformation("Retreived data was null. Trying to get from Database.");
        var storedRating = await _ratings.FirstOrDefaultAsync(e => e.SubjectId == subjectId, token);
        if (storedRating != null)
        {
            _logger.LogTrace($"Received data from the Database. Adding to the Cache storage.");
            _cacheService.SetRatingValue(subjectId, storedRating);
            return storedRating;
        }
        _logger.LogTrace($"No data has been found for the Subject with Id '{subjectId}'.");
        return null;
    }
    
    private async Task Recalculate(int subjectId, EntityType type, CancellationToken token)
    {
        // Stage 1: Recalculate a Rating of the Participant
        // there should be no problems with dividing by 0, since this method is gonna be called when at least 1 Update has already been stored
        float newValue = await _ratings
            .Include(e => e.Updates)
            .Where(e => e.SubjectId == subjectId)
            .Select(e => new { Sum = e.Updates.Sum(x => x.Value), Count = e.Updates.Count() })
            .Select(e => e.Sum / e.Count)
            .FirstOrDefaultAsync(token);

        var rating = await GetRating(subjectId, token);
        ArgumentNullException.ThrowIfNull(rating);

        rating.SetUpdatedValue(newValue);
        _ratings.Update(rating);

        await storage.SaveChangesAsync(token);

        // Stage 2: recalculating a Rating of the related User, IF it`s a User entity
        if (type is EntityType.Gamer || type is EntityType.Organizer)
        {
            var userInfo = await _userInfoRepo.GetByFilter(e => e.Id == subjectId, token);
            // TODO recalculating for a Gamer OR Organizer
        }
    }
}
