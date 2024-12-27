using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public class RatingUpdate : Entity<int>
{
    public float Value { get; protected set; }
    /// <summary>
    /// A relation to the Rating entity.
    /// </summary>
    public int RatingId { get; protected set; }
    public int AuthorId { get; }
    public int SubjectId { get; protected set; }
    public int EventId { get; }
    public EntityType EntityType { get; }
    public DateTime CreationDate { get; }
    public DateTime? UpdateDate { get; protected set; }

    public RatingUpdate(float value, int authorId, int subjectId, int eventId, EntityType entityType, DateTime creationDate)
    {
        Value = value;
        AuthorId = authorId;
        SubjectId = subjectId;
        EventId = eventId;
        EntityType = entityType;
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
