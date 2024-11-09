namespace RatingService.Domain.Models.ValueObjects.Identifiers;

public class UserInfoId
{
    public int Id { get; }
    private UserInfoId(int id) => Id = id;

    public static UserInfoId Create(int id) => new UserInfoId(id);
}