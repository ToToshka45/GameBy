using RatingService.Domain.Entities.Ratings;
using RatingService.Domain.Enums;

namespace RatingService.Application.Services.Abstractions;

internal interface ICachingService
{
    bool TryRetrieve(int ratingId, EntityType type, out RatingBase? rating);
    void SetRatingValue(int ratingId, EntityType type, RatingBase ratingValue);
}
