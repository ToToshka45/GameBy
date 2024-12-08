using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Entities;

public class EventRatingUpdate : Entity<int>
{
    public int AuthorId { get; }
    public int ExternalEventId { get; }
    public DateTime CreationDate { get; }
    public EventRating Rating { get; }

    public EventRatingUpdate(int authorId, DateTime creationDate, EventRating rating, int eventId)
    {
        AuthorId = authorId;
        CreationDate = creationDate;
        Rating = rating;
        ExternalEventId = eventId;
    }

    private EventRatingUpdate() { }
}
