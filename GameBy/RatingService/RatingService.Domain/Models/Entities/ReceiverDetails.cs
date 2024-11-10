using RatingService.Domain.Abstractions;
using RatingService.Domain.Enums;

namespace RatingService.Domain.Models.Entities
{
    /// <summary>
    /// Details about the Receiver of Feedback, specifying what type of Entity the Receiver is (Organizer, Gamer, Event) 
    /// and storing its Id in the system. 
    /// </summary>
    public class ReceiverDetails : ValueObject
    {
        public int EntityId { get; }
        public EntityType EntityType { get; }
        private ReceiverDetails(int entityId, EntityType entityType)
        {
            EntityId = entityId;
            EntityType = entityType;
        }

        public static ReceiverDetails Create(int entityId, EntityType entityType) => new ReceiverDetails(entityId, entityType);

        protected override IEnumerable<object> GetAtomicValues()
        {
            return [EntityId, EntityType];
        }
        
        // TODO: create a validation method to check if the provided Id belongs to the provided EntityType,
        // probably adressing some API interface which stores all the occurances of different entities
    }
}