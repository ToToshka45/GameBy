using RatingService.Domain.Enums;

namespace RatingService.Domain.Entities;

public class UserRating : Rating
{
    public int UserId { get; }
    public Category Category { get; }
    public UserRating(int userId, Category category)
    {
        UserId = userId;
        Category = category;
    }

    private UserRating() { }
}

