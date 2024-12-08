using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public class UserRatingUpdate : Entity<int>
{
    public int AuthorId { get; }
    public int ExternalEventId { get; }
    public int RatingOwnerId { get; }
    public DateTime CreationDate { get; }
    public UserRating Rating { get; }

    public UserRatingUpdate(int authorId, DateTime creationDate, UserRating rating, 
        int eventId, int userId)
    {
        AuthorId = authorId;
        CreationDate = creationDate;
        Rating = rating;
        ExternalEventId = eventId;
        RatingOwnerId = userId;
    }

    private UserRatingUpdate() { }
}
