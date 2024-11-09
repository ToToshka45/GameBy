namespace RatingService.Domain.Models.ValueObjects.Identifiers;

public class EventId
{
    public int Id { get; }
    private EventId(int id) => Id = id;

    public static EventId Create(int id) => new EventId(id);
}