using RatingService.Common.Enums;
using RatingService.Domain.Exceptions;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities.Ratings;

public abstract class RatingBase : Entity<int>
{
    /// <summary>
    /// Stores a value from 0 to 5.00.
    /// </summary>
    public float Value { get; protected set; }

    protected RatingBase()
    { 
    }

    // TODO: decide if this logic must be in a separate service and not in the entity itself
    public virtual void Recalculate() { }

    public void SetUpdatedValue(float newValue)
    {
        Validate(newValue);
        Value = newValue;
    }

    private void Validate(float value)
    {
        InvalidRatingValueException.ThrowIfInvalid(() => value < 0, "The value is less then 0.");
        InvalidRatingValueException.ThrowIfInvalid(() => value > 5, "The value is more then 5.");
    }

}

