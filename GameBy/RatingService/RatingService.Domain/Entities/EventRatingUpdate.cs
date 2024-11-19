using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Entities;

public class EventRatingUpdate : Entity<int>
{
    public AuthorId AuthorId { get; }
    public ExternalEventId EventId { get; }
    public DateTime CreationDate { get; }
    public Rating Value { get; }

    public EventRatingUpdate(int id, AuthorId authorId, DateTime creationDate, Rating value, ExternalEventId eventId) : base(id)
    {
        AuthorId = authorId;
        CreationDate = creationDate;
        Value = value;
        EventId = eventId;
    }

}
