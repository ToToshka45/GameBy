namespace RatingService.Domain.Abstractions;

public abstract class Entity : IEquatable<Entity>
{
    public int Id { get; }
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        var entity = obj as Entity;
        return this.Equals(entity);
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    public bool Equals(Entity? other)
    {
        return other is null ? false : Id.Equals(other.Id);
    }
}
