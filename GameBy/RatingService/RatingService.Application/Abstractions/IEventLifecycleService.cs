using RatingService.Application.Models.Dtos;
using RatingService.Domain.Aggregates;

namespace RatingService.Application.Abstractions;

public interface IEventLifecycleService
{
    Task<EventInfo> AddNewEventAsync(CreateEventDto newEvent, CancellationToken token);
    Task<ICollection<EventInfo>> GetEventsAsync(CancellationToken token);
    Task<EventInfo?> GetEventInfoAsync(int id, CancellationToken token);
}
