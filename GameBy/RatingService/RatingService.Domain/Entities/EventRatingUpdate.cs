namespace RatingService.Domain.Entities;

public class EventRatingUpdate : RatingUpdate
{
    public int RatingId { get; private set; }
    public EventRatingUpdate(float value, int authorId, int externalEventId, DateTime creationDate)
        : base(value, authorId, subjectId: externalEventId, creationDate)
    {
        SubjectId = externalEventId;
    }

    private EventRatingUpdate() { }
    public void SetRatingRelation(int ratingId) => RatingId = ratingId;
}
