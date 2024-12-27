using Microsoft.Extensions.Caching.Memory;
using RatingService.Domain.Entities;

namespace RatingService.Application.Services;

public sealed class InMemoryCachingService
{
    private readonly IMemoryCache _ratings;
    //private readonly IMemoryCache<int, float> _eventRatings;

    public InMemoryCachingService(IMemoryCache ratings)
    {
        _ratings = ratings;
    }

    public bool TryRetrieve(int ratingId, out Rating? rating)
    {
        rating = (Rating?)_ratings.Get(ratingId);
        return rating != null;
    }

    public void SetRatingValue(int ratingId, Rating ratingValue)
    {
        _ratings.CreateEntry(ratingId).Value = ratingValue;
    }
}
