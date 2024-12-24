using RatingService.Domain.Abstractions;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;

namespace RatingService.Domain.Abstraction;

public interface IEventLifecycleRepository : IRepository<EventInfo>
{
    Task<IEnumerable<Participant>> GetParticipantsByEventId(int eventId, CancellationToken cancellationToken);
    Task<Participant?> GetParticipantByEventId(int eventId, int externalParticipantId, CancellationToken cancellationToken);
}