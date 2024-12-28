using Microsoft.EntityFrameworkCore;
using RatingService.Domain.Abstraction;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;
using RatingService.Infrastructure.Abstractions;
using RatingService.Infrastructure.DataAccess;

namespace RatingService.Infrastructure.Repositories;

public class EventLifecycleRepository(RatingServiceDbContext storage) 
    : BaseRepository<EventInfo>(storage), IEventLifecycleRepository
{
    private readonly DbSet<Participant> _participantStorage = storage.Set<Participant>();

    public async Task<IEnumerable<Participant>> GetParticipantsByEventId(int eventId, CancellationToken cancellationToken)
    {
        return await _participantStorage.Where(e => e.EventId == eventId).ToListAsync(cancellationToken);
    }

    public async Task<Participant?> GetParticipantByEventId(int eventId, int externalParticipantId, CancellationToken cancellationToken)
    {
        return await _participantStorage.FirstOrDefaultAsync(p => p.EventId == eventId && p.Id == externalParticipantId, 
            cancellationToken);
    }
}
