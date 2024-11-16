namespace RatingService.Domain.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public static bool operator ==(ValueObject? a, ValueObject? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(ValueObject? a, ValueObject? b) =>
        !(a == b);

    #region Equals

    public virtual bool Equals(ValueObject? other) =>
        other is not null && ValuesAreEqual(other);

    public override bool Equals(object? obj) =>
        obj is ValueObject valueObject && ValuesAreEqual(valueObject);

    private bool ValuesAreEqual(ValueObject valueObject) =>
        GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());

    #endregion

    /// <summary>
    /// Creates a HashCode value made of fields inside of a Value Object.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() =>
        GetAtomicValues().Aggregate(
            default(int),
            (hashcode, value) =>
                HashCode.Combine(hashcode, value.GetHashCode()));

    /// <summary>
    /// Iterates and returns all the fields that need to be checked for equality OR used as the material for  HashCode production.
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<object> GetAtomicValues();

}
