using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public abstract class RatingUpdate : Entity<int>
{
    public int RatingId { get; private set; }
    public int AuthorId { get; }
    public int EventId { get; }
    public DateTime CreationDate { get; }

    public RatingUpdate(int authorId, DateTime creationDate, int eventId)
    {
        AuthorId = authorId;
        CreationDate = creationDate;
        EventId = eventId;
    }

    protected RatingUpdate() { }

    public void SetRatingRelation(int ratingId) => RatingId = ratingId;
}
