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

    //public async Task<int?> AddParticipant(Participant participant, CancellationToken cancellationToken)
    //{
    //    await _participantStorage.AddAsync(participant, cancellationToken);
    //    await storage.SaveChangesAsync(cancellationToken);
    //    var storedParticipant = await _participantStorage.FirstOrDefaultAsync(p => p.Id == participant.Id, cancellationToken);
    //    return storedParticipant != null ? storedParticipant.Id : null;
    //}

    public async Task<IEnumerable<Participant>> GetParticipantsByEventId(int eventId, CancellationToken cancellationToken)
    {
        return await _participantStorage.Where(e => e.EventInfoId == eventId).ToListAsync(cancellationToken);
    }

    public async Task<Participant?> GetParticipantByEventId(int eventId, int participantId, CancellationToken cancellationToken)
    {
        return await _participantStorage.FirstOrDefaultAsync(
            p => p.EventInfoId == eventId 
                && p.Id == participantId,
            cancellationToken);
    }

    public async Task RemoveParticipantByEventId(int eventId, int participantId, CancellationToken cancellationToken)
    {
        var entity = await GetParticipantByEventId(eventId, participantId, cancellationToken);
        if (entity == null) { return; }
        _participantStorage.Remove(entity);
        await storage.SaveChangesAsync(cancellationToken);
    }
}
