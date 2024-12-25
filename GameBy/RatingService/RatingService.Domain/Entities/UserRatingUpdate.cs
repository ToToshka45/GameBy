using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public class UserRatingUpdate : RatingUpdate
{
    public int RatingOwnerId { get; }

    public UserRatingUpdate(int authorId, DateTime creationDate, int eventId, int userId) 
        : base(authorId, creationDate, eventId)
    {
        RatingOwnerId = userId;
    }
    private UserRatingUpdate() { }
}
