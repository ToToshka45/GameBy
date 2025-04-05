using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories;

public class EventsRepository : EfRepository<Event>
{
    private readonly DbSet<Event> _eventRepo;
    public EventsRepository(DataContext dataContext, ILogger<EventsRepository> logger) : base(dataContext, logger)
    {
        _eventRepo = dataContext.Set<Event>();
    }
    
    public override async Task<Event?> GetByIdAsync(int id)
    {
        var entity = await _eventRepo.AsNoTracking()
                                     .Include(e => e.Participants)
                                     .FirstOrDefaultAsync(x => x.Id == id);

        return entity;
    }
}
