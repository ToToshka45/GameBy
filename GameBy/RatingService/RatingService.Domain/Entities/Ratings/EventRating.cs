namespace RatingService.Domain.Entities.Ratings;

public class EventRating : IntermediateRating
{
    public int ExternalEventId { get; }
    public EventRating(int externalEventId) 
    {
        ExternalEventId = externalEventId;
    }

    private EventRating() { }
}

