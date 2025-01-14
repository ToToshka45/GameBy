using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using RatingService.Application.Services.Abstractions;
using RatingService.Domain.Entities.Ratings;
using RatingService.Domain.Enums;

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

    public bool TryRetrieve(int ratingId, EntityType type, out RatingBase? rating)
    {
        _logger.LogInformation($"Retrieving a cached Rating with Id '{ratingId}'.");
        rating = (RatingBase?)_ratings.Get(new { ratingId, type }.GetHashCode());
        return rating != null;
    }

    public void SetRatingValue(int ratingId, EntityType type, RatingBase ratingValue) =>
        _ratings.Set(new { ratingId, type }.GetHashCode(), ratingValue);
}
