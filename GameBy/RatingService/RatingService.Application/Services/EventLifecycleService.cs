using Microsoft.Extensions.Logging;
using RatingService.Application.Abstractions;
using RatingService.Application.Configurations.Mappings;
using RatingService.Application.Models.Dtos;
using RatingService.Domain.Abstractions;
using RatingService.Domain.Aggregates;

namespace RatingService.Application.Services;

public class EventLifecycleService : IEventLifecycleService
{
    private readonly IRepository<EventInfo> _eventRepo;

    private readonly ILogger<EventLifecycleService> _logger;
    public EventLifecycleService(IRepository<EventInfo> eventRepo, ILogger<EventLifecycleService> logger)
    {
        _eventRepo = eventRepo;
        _logger = logger;
    }

    public async Task<EventInfo> AddNewEventAsync(CreateEventDto newEvent, CancellationToken token)
    {
        try
        {
            var eventInfo = newEvent.ToEventInfo();
            return await _eventRepo.Add(eventInfo, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while adding a new Event instance.");
            throw;
        }
    }

    public async Task<EventInfo?> GetEventInfoAsync(int id, CancellationToken token)
    {
        try
        {
            var eventInfo = await _eventRepo.GetById(id, token);
            return eventInfo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while retrieveing a stored Event instance.");
            throw;
        }
    }

    public async Task<ICollection<EventInfo>> GetEventsAsync(CancellationToken token)
    {
        return await _eventRepo.GetAll(token);
    }

    public async Task AddParticipantsAsync(CancellationToken token)
    {

    }
}
