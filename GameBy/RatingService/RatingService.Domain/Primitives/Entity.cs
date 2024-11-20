namespace RatingService.Domain.Primitives;

public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
{
    public TId Id { get; }

    protected Entity()
    {
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        var entity = obj as Entity<TId>;
        return Equals(entity);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(Entity<TId>? other)
    {
        return other is null ? false : Id.Equals(other.Id);
    }
}
