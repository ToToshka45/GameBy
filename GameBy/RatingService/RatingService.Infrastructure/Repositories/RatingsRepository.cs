using Microsoft.EntityFrameworkCore;
using RatingService.Application.Services;
using RatingService.Domain.Abstractions;
using RatingService.Domain.Entities;
using RatingService.Infrastructure.DataAccess;

namespace RatingService.Infrastructure.Repositories;

public class RatingsRepository(RatingServiceDbContext storage, InMemoryCachingService cacheService) : IRatingsRepository
{
    private readonly DbSet<Rating> _ratings = storage.Set<Rating>();
    private readonly InMemoryCachingService _cacheService = cacheService;

    public async Task AddAndRecalculate(RatingUpdate update, CancellationToken token)
    {
        // level of isolation is Read Commited by default in PostreSQL, should be sufficient
        using var tran = await storage.Database.BeginTransactionAsync(token);

        var rating = await _ratings.Include(e => e.Updates).FirstOrDefaultAsync(e => e.SubjectId == update.SubjectId);
        if (rating == null) { return; }
        rating.AddRatingUpdate(update);
        await storage.SaveChangesAsync();

        await Recalculate(update.SubjectId, token);

        await tran.CommitAsync(token);

        // set cache
        _cacheService.SetRatingValue(update.RatingId, rating);
    }

    public async ValueTask<Rating?> GetRating(int subjectId, CancellationToken token)
    {
        if (_cacheService.TryRetrieve(subjectId, out var rating)) return rating;
        var storedRating = await _ratings.FirstOrDefaultAsync(e => e.SubjectId == subjectId, token);
        if (storedRating != null)
        {
            _cacheService.SetRatingValue(subjectId, storedRating);
            return storedRating;
        }
        return null;
    }

    private async Task Recalculate(int subjectId, CancellationToken token)
    {
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
    }
}
