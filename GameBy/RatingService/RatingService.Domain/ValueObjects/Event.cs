using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.ValueObjects
{
    public class Event : ValueObject
    {
        public EventId EventId { get; }
        public Category Category { get; }

        public Event(EventId eventId, Category category)
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