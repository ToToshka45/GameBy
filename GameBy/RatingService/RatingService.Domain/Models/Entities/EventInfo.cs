using RatingService.Domain.Models.ValueObjects;
using System.Reflection;

namespace RatingService.Domain.Models.Entities;

public class EventInfo
{    
    public int Id { get; private set; }
    public EventId UserId { get; }
    public Rating Rating { get; }
    private EventInfo(UserId userId, Rating rating) { UserId = userId; Rating = rating; }

    public static EventInfo Create(UserId id, Rating rating) => new EventInfo(id, rating);
}

