using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Aggregates;

public class RatingUpdate : AggregateRoot<int>
{
    public UserId UserId { get; }
    public Event Event { get; }

    public DateTime CreationDate { get; }
    public Category Category { get; }
    public Rating Value { get; }

    public RatingUpdate(int id, UserId userId, DateTime creationDate, Rating value, Category category, Event eventEntity) : base(id)
    {
        UserId = userId;
        CreationDate = creationDate;
        Value = value;
        Category = category;
        Event = eventEntity;
    }

}
