using RatingService.Domain.Primitives;

namespace RatingService.Domain.ValueObjects.Identifiers;
public class AuthorId : BaseEntityId
{
    public AuthorId(int id) : base(id) { }
}