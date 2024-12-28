using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using RatingService.Application.Services.Abstractions;
using RatingService.Domain.Entities;

namespace RatingService.Application.Services;
 
// TODO: add Redis implementation for external caching

public sealed class InMemoryCachingService : ICachingService
{
    private readonly IMemoryCache _ratings;
    private readonly ILogger<InMemoryCachingService> _logger;
    //private readonly IMemoryCache<int, float> _eventRatings;

    public InMemoryCachingService(IMemoryCache ratings, ILogger<InMemoryCachingService> logger)
    {
        _ratings = ratings;
        _logger = logger;
    }

    public bool TryRetrieve(int ratingId, out Rating? rating)
    {
        _logger.LogInformation($"Retrieving a cached Rating with Id '{ratingId}'.");
        rating = (Rating?)_ratings.Get(ratingId);
        return rating != null;
    }

    public void SetRatingValue(int ratingId, Rating ratingValue)
    {
        _ratings.CreateEntry(ratingId).Value = ratingValue;
    }
}
