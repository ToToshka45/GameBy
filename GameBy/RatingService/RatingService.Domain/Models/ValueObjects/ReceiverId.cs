namespace RatingService.Domain.Models.ValueObjects
{
    public class ReceiverId
    { 
        public int Id { get; }
        private ReceiverId(int id) => Id = id;
        
        public static ReceiverId Create(int id) => new ReceiverId(id);
    }
}