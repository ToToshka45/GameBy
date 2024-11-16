using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Aggregates;

public class EventRating : Entity<int>
{
    public EventId EventId { get; }
    public Rating RatingValue { get; }
    public Category Category { get; }

    public EventRating(
        int id,
        EventId eventId,
        Rating value,
        Category category) : base(id)
    {
        EventId = eventId;
        RatingValue = value;
        Category = category;
    }
}

