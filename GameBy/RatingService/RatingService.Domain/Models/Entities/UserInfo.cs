using RatingService.Domain.Models.ValueObjects;

namespace RatingService.Domain.Models.Entities;

public class UserInfo
{    
    public int Id { get; private set; }
    public UserId UserId { get; }
    public Rating Rating { get; }
    private UserInfo(UserId userId, Rating rating) { UserId = userId; Rating = rating; }

    public static UserInfo Create(UserId id, Rating rating) => new UserInfo(id, rating);
}

