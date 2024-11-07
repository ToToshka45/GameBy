using RatingService.Domain.Exceptions;

namespace RatingService.Domain.Models.ValueObjects;

public class Rating
{
    public float Value { get; }
    private Rating(float value)
    {
        Validate(value);
        Value = value;
    }

    public Rating Create(float value) => new Rating(value);

    private void Validate(float value)
    {
        InvalidRatingValueException.ThrowIfInvalid(() => value < 0, "The value is less then 0.");
        InvalidRatingValueException.ThrowIfInvalid(() => value < 5, "The value is more then 5.");
    }
}

