using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Aggregates;

public class RatingUpdate : AggregateRoot<int>
{
    public UserId UserId { get; }
    public EventId EventId { get; }

    public DateTime CreationDate { get; }
    public Category Category { get; }

    // values
    public Rating RatingValue { get; }

    public RatingUpdate(int id, UserId userId, DateTime creationDate, Rating value, Category category, EventId eventId) : base(id)
    {
        UserId = userId;
        CreationDate = creationDate;
        RatingValue = value;
        Category = category;
        EventId = eventId;
    }

}
