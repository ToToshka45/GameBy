using RatingService.Domain.Entities;

namespace RatingService.Application.Services.Abstractions;

internal interface ICachingService
{
    bool TryRetrieve(int ratingId, out Rating? rating);
    void SetRatingValue(int ratingId, Rating ratingValue);
}
