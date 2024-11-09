using RatingService.Domain.Enums;

namespace RatingService.Domain.Models.ValueObjects
{
    public class ReceiverInfo
    { 
        public int EntityId { get; }
        public EntityType EntityType { get; }
        private ReceiverInfo(int entityId, EntityType entityType)
        {
            EntityId = entityId;
            EntityType = entityType;
        }
        
        public static ReceiverInfo Create(int entityId, EntityType entityType) => new ReceiverInfo(entityId, entityType);
    }
}