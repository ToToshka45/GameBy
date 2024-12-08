using RatingService.Domain.Enums;
using RatingService.Domain.Exceptions;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public abstract class Rating : Entity<int>
{
    public Category Category { get; }
    public float Value { get; protected set; }
    protected Rating(Category category)
    {
        Category = category;
        Value = 0;
    }

    protected Rating() { }

    // TODO: decide if this logic must be in a separate service and not in the entity itself
    public float Recalculate(float value)
    {
        Validate(value);
        var recalculated = value;
        // recalculating logic

        return recalculated;
    }

    private void Validate(float value)
    {
        InvalidRatingValueException.ThrowIfInvalid(() => value < 0, "The value is less then 0.");
        InvalidRatingValueException.ThrowIfInvalid(() => value > 5, "The value is more then 5.");
    }

}

