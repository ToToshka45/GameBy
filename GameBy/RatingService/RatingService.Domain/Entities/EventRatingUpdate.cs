using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public class EventRatingUpdate : Entity<int>
{
    public int AuthorId { get; }
    public int EventId { get; }
    public DateTime CreationDate { get; }
    public EventRating Rating { get; }

    public EventRatingUpdate(int authorId, DateTime creationDate, EventRating rating, int eventId)
    {
        AuthorId = authorId;
        CreationDate = creationDate;
        Rating = rating;
        EventId = eventId;
    }

    private EventRatingUpdate() { }
}
