using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.ValueObjects
{
    public class Event : ValueObject
    {
        public Event EventId { get; }
        public Category Category { get; }

        public Event(Event eventId, Category category)
        {
            EventId = eventId;
            Category = category;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return EventId;
            yield return Category.ToString();
        }
    }
}