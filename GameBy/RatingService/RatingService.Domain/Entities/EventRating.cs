using RatingService.Domain.Enums;

namespace RatingService.Domain.Entities;

public class EventRating : Rating
{
    public int EventId { get; }
    public EventRating(int eventId)
    {
        EventId = eventId;
    }

    private EventRating() { }
}

