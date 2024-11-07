namespace RatingService.Domain.Models.ValueObjects;
public class UserId
{ 
    public int Id { get; }
    private UserId(int id) => Id = id;

    public static UserId Create(int id) => new UserId(id);
}