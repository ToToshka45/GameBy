using RatingService.Domain.Enums;

namespace RatingService.Domain.Entities;

public class UserRating : Rating
{
    public int UserId { get; }
    public UserRating(int userId, Category category) : base(category)
    {
        UserId = userId;
    }

    private UserRating() { }
}

