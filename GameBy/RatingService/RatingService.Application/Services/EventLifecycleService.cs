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

    public async Task AddNewEventAsync(CreateEventDto @event, CancellationToken token)
    {
        try
        {
            var eventInfo = @event.ToEventInfo();
            await _eventRepo.Add(eventInfo, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while adding a new Event instance.");
            throw;
        }
    }

    public async Task AddParticipantsAsync(CancellationToken token)
    {

    }
}
