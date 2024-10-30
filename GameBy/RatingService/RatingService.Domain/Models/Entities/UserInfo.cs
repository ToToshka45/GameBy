using RatingService.Domain.Models.ValueObjects;

namespace RatingService.Domain.Models.Entities;

public class UserInfo
{    
    public UserInfoId Id { get; } = new UserInfoId();
    public UserId UserId { get; }
    public Rating Rating { get; }
    private UserInfo(UserId userId) { UserId = userId; }

    public static UserInfo Create(UserId id) => new UserInfo(id);
}

