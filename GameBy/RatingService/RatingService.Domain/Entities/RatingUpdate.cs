using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public abstract class RatingUpdate : Entity<int>
{
    /// <summary>
    /// A relation to the Rating entity.
    /// </summary>
    public int RatingId { get; protected set; }
    public float Value { get; protected set; }
    public int AuthorId { get; }
    public int SubjectId { get; protected set; }
    public int ExternalEventId { get; }
    //public EntityType EntityType { get; }
    public DateTime CreationDate { get; }
    public DateTime? UpdateDate { get; protected set; }

    public RatingUpdate(float value, int authorId, int subjectId, int externalEventId, DateTime creationDate)
    {
        Value = value;
        AuthorId = authorId;
        SubjectId = subjectId;
        ExternalEventId = externalEventId;
        //EntityType = entityType;
        CreationDate = creationDate;
    }

    protected RatingUpdate() { }

    public void SetRatingRelation(int ratingId) => RatingId = ratingId;
    public void SetNewValue(float updatedValue, DateTime updatedAt)
    {
        Value = updatedValue;
        UpdateDate = updatedAt;
    }
}
