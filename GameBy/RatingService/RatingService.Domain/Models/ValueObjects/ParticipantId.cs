namespace RatingService.Domain.Models.ValueObjects
{
    public class ParticipantId
    { 
        public int Id { get; }
        private ParticipantId(int id) => Id = id;
        
        public static ParticipantId Create(int id) => new ParticipantId(id);
    }
}