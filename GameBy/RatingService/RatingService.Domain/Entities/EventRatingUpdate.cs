namespace RatingService.Domain.Entities;

public class EventRatingUpdate : RatingUpdate
{
    public EventRatingUpdate(float value, int authorId, int externalEventId, DateTime creationDate)
        : base(value, authorId, subjectId: externalEventId, externalEventId, creationDate)
    {
        SubjectId = externalEventId;
    }

    private EventRatingUpdate() { }
}
