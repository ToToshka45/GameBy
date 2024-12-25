using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public class EventRatingUpdate : RatingUpdate
{
    public EventRatingUpdate(int authorId, DateTime creationDate, int eventId) 
        : base(authorId, creationDate, eventId)
    {
    }

    private EventRatingUpdate() { }
}
