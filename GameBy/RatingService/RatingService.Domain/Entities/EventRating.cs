using RatingService.Domain.Enums;

namespace RatingService.Domain.Entities;

public class EventRating : Rating
{
    public int EventId { get; }
    public EventRating(int eventId, Category category) : base(category)
    {
        EventId = eventId;
    }

    private EventRating() { }
}

