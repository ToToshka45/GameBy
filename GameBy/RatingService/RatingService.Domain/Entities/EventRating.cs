using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;

namespace RatingService.Domain.Aggregates;

public class EventRating : Entity<int>
{
    public Event EventId { get; }
    public Rating RatingValue { get; }
    public Category Category { get; }

    public EventRating(
        int id,
        Event eventId,
        Rating value,
        Category category) : base(id)
    {
        EventId = eventId;
        RatingValue = value;
        Category = category;
    }
}

