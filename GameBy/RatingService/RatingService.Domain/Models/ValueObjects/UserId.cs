namespace RatingService.Domain.Models.ValueObjects
{
    /// <summary>
    /// The Id of the Entity, which is described in the provided Info. Can be a User, an Event, or other Entity if it exists.
    /// </summary>
    public class UserId
    { 
        public Guid Id { get; } = Guid.NewGuid();
    }
}