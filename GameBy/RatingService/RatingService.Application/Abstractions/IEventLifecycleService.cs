using RatingService.Application.Models.Dtos.Events;
using RatingService.Application.Models.Dtos.Participants;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;

namespace RatingService.Application.Abstractions;

public interface IEventLifecycleService
{
    Task<int?> AddNewEventAsync(CreateEventDto dto, CancellationToken token);
    Task<ICollection<GetEventDto>> GetEventsAsync(CancellationToken token);
    Task<GetEventDto?> GetEventByIdAsync(int id, CancellationToken token);
    Task AddParticipantAsync(int eventId, AddParticipantDto dto, CancellationToken token);
    Task FinalizeEventAsync(FinalizeEventDto dto, CancellationToken token);
}
