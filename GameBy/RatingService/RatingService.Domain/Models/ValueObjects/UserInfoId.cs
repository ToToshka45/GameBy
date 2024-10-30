namespace RatingService.Domain.Models.ValueObjects
{
    /// <summary>
    /// The Id of the Entity, which is described in the provided Info. Can be a User, an Event, or other Entity if it exists.
    /// </summary>
    public class UserInfoId
    { 
        public Guid Id { get; }
    }
}