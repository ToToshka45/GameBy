using RatingService.Domain.Models.ValueObjects;

namespace RatingService.Domain.Models.Entities;

public class Rating
{
    public RatingValue Value { get; }
    private Rating(RatingValue value)
    {
        Value = value;
    }

    public Rating Create(RatingValue value) => new Rating(value);
}

