namespace RatingService.Domain.Models.ValueObjects;
public class AuthorId
{ 
    public int Id { get; }
    private AuthorId(int id) => Id = id;

    public static AuthorId Create(int id) => new AuthorId(id);
}