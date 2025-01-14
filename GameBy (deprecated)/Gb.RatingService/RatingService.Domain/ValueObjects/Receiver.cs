using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.ValueObjects
{
    /// <summary>
    /// Details about the Receiver of Feedback, specifying what type of Entity the Receiver is (Organizer, Gamer, Event) 
    /// and storing its Id in the system. 
    /// </summary>
    public class Receiver : ValueObject
    {
        public int SubjectId { get; }
        public EntityType EntityType { get; }

        public Receiver(int entityId, EntityType entityType)
        {
            SubjectId = entityId;
            EntityType = entityType;
        }

        private Receiver() { }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return SubjectId;
            yield return EntityType.ToString();
        }

        // TODO: create a validation method to check if the provided Id belongs to the provided EntityType,
        // probably adressing some API interface which stores all the occurances of different entities
    }
}