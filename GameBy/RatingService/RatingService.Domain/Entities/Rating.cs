using RatingService.Domain.Enums;
using RatingService.Domain.Exceptions;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public class Rating : Entity<int>
{
    public Category Category { get; }
    public float Value { get; }
    public Rating(float value, Category category)
    {
        Validate(value);
        Category = category;
        Value = value;
    }

    private Rating() { }

    private void Validate(float value)
    {
        InvalidRatingValueException.ThrowIfInvalid(() => value < 0, "The value is less then 0.");
        InvalidRatingValueException.ThrowIfInvalid(() => value > 5, "The value is more then 5.");
    }
}

