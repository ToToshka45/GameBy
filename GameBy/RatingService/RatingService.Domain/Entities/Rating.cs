using RatingService.Domain.Enums;
using RatingService.Domain.Exceptions;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public class Rating : Entity<int>
{
    public float Value { get; protected set; }
    public int SubjectId { get; }
    public EntityType EntityType { get; }

    private List<RatingUpdate> _updates = [];
    public IReadOnlyList<RatingUpdate> Updates => _updates;

    public Rating(int subjectId, EntityType entityType)
    {
        SubjectId = subjectId;
        EntityType = entityType;
    }

    private Rating() { }

    // TODO: decide if this logic must be in a separate service and not in the entity itself
    public virtual void Recalculate()
    {
        var sum = _updates.Sum(u => u.Value);
        var count = _updates.Count();
        Value = sum / count;
    }

    public void AddRatingUpdate(RatingUpdate update) => _updates.Add(update);
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

