using RatingService.Domain.Primitives;

namespace RatingService.Domain.ValueObjects.Identifiers;

public abstract class BaseEntityId : ValueObject
{
    public int Value { get; }
    protected BaseEntityId(int value) => Value = value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
