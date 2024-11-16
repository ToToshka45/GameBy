using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Aggregates;

public class UserRating : Entity<int>
{
    public UserId UserId { get; }
    public Rating RatingValue { get; }
    public Category Category { get; }

    public UserRating(
        int id,
        UserId userId,
        Rating value,
        Category category) : base(id)
    {
        UserId = userId;
        RatingValue = value;
        Category = category;
    }
}

