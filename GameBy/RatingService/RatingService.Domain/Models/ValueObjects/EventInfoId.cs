namespace RatingService.Domain.Models.ValueObjects;

public class EventInfoId
{
    public int Id { get; }
    private EventInfoId(int id) => Id = id;

    public static EventInfoId Create(int id) => new EventInfoId(id);
}