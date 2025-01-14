using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public abstract class RatingUpdate : Entity<int>
{
    public float Value { get; protected set; }
    public int AuthorId { get; }
    public int SubjectId { get; protected set; }
    //public EntityType EntityType { get; }
    public DateTime CreationDate { get; }
    public DateTime? UpdateDate { get; protected set; }

    public RatingUpdate(float value, int authorId, int subjectId, DateTime creationDate)
    {
        Value = value;
        AuthorId = authorId;
        SubjectId = subjectId;
        CreationDate = creationDate;
    }

    protected RatingUpdate() { }

    public void SetNewValue(float updatedValue, DateTime updatedAt)
    {
        Value = updatedValue;
        UpdateDate = updatedAt;
    }
}
