using RatingService.Application.Models.Dtos.Events;
using RatingService.Application.Models.Dtos.Participants;
using RatingService.Domain.Entities;

namespace RatingService.Application.Services.Abstractions;

public interface IEventLifecycleService
{
    Task<int?> AddNewEventAsync(CreateEventDto dto, CancellationToken token);
    Task<ICollection<GetEventDto>> GetEventsAsync(CancellationToken token);
    Task<GetEventDto?> GetEventByIdAsync(int id, CancellationToken token);
    Task<int?> AddParticipantAsync(int eventId, AddParticipantDto dto, CancellationToken token);
    Task<IEnumerable<Participant>> GetParticipantsByEventIdAsync(int eventId, CancellationToken token);
    Task FinalizeEventAsync(FinalizeEventDto dto, CancellationToken token);
}
