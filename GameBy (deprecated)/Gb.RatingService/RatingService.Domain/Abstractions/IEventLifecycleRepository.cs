using RatingService.Domain.Abstractions;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;

namespace RatingService.Domain.Abstraction;

public interface IEventLifecycleRepository : IRepository<EventInfo>
{
    //Task<int?> AddParticipant(Participant participant, CancellationToken cancellationToken);
    Task<IEnumerable<Participant>> GetParticipantsByEventId(int eventId, CancellationToken cancellationToken);
    Task<Participant?> GetParticipantByEventId(int eventId, int externalParticipantId, CancellationToken cancellationToken);
    Task RemoveParticipantByEventId(int eventId, int externalParticipantId, CancellationToken cancellationToken);
}