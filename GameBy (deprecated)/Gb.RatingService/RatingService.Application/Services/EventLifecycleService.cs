using Microsoft.Extensions.Logging;
using RatingService.Application.Configurations.Mappings;
using RatingService.Application.Models.Dtos.Events;
using RatingService.Application.Models.Dtos.Participants;
using RatingService.Application.Models.Dtos.Ratings;
using RatingService.Application.Services.Abstractions;
using RatingService.Domain.Abstraction;
using RatingService.Domain.Abstractions;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;

namespace RatingService.Application.Services;

public class EventLifecycleService : IEventLifecycleService
{
    //private readonly IRepository<EventInfo> _eventRepo;
    private readonly IEventLifecycleRepository _eventRepo;
    private readonly IRatingsProcessingService _ratingsProcessingService;
    private readonly IRepository<UserInfo> _userRepo;
    private readonly ILogger<EventLifecycleService> _logger;

    public EventLifecycleService(IEventLifecycleRepository eventRepo, IRepository<UserInfo> userRepo, ILogger<EventLifecycleService> logger, IRatingsProcessingService ratingsProcessingService)
    {
        _eventRepo = eventRepo;
        _logger = logger;
        _userRepo = userRepo;
        _ratingsProcessingService = ratingsProcessingService;
    }

    public async Task<GetEventInfoDto?> AddNewEventAsync(CreateEventDto newEvent, CancellationToken token)
    {
        try
        {
            var eventInfo = newEvent.ToEventInfo();
            var savedEvent = await _eventRepo.Add(eventInfo, token);
            if (savedEvent is null) return null;
            return savedEvent.ToGetEventInfoDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while adding a new Event instance.");
            throw;
        }
    }

    public async Task<GetEventInfoDto?> GetEventByIdAsync(int id, CancellationToken token)
    {
        try
        {
            var eventInfo = await _eventRepo.GetById(id, token);
            if (eventInfo == null) { return null; }
            return eventInfo.ToGetEventInfoDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while retrieveing a stored Event instance.");
            throw;
        }
    }

    public async Task<ICollection<GetEventInfoDto>> GetEventsAsync(CancellationToken token)
    {
        var events = await _eventRepo.GetAll(token);
        return events.ToDtoList();
    }

    public Task FinalizeEventAsync(FinalizeEventDto dto, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    // Participants 

    public async Task<GetParticipantDto?> AddParticipantAsync(int eventId, AddParticipantDto dto, CancellationToken token)
    {
        try
        {
            var storedEvent = await _eventRepo.GetEntityWithIncludesAsync(eventId, token, [e => e!.Participants]);
            if (storedEvent is null) return null;

            storedEvent.ValidateParticipant(dto.ExternalParticipantId);
            var participant = dto.ToParticipant();
            storedEvent.AddParticipant(participant);
            await _eventRepo.SaveChangesAsync(token);

            if (await _eventRepo.GetParticipantByEventId(eventId, dto.ExternalParticipantId, token) is Participant entity) 
                return entity.ToDto();

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while trying to add a new Participant to an Event.");
            throw;
        }
    }

    public async Task<GetParticipantDto?> GetParticipantByEventIdAsync(int eventId, int participantId, CancellationToken token)
    {
        var participant = await _eventRepo.GetParticipantByEventId(eventId, participantId, token);
        if (participant is null) return null;
        return participant.ToDto();
    }

    public async Task<IEnumerable<GetParticipantDto>> GetParticipantsByEventIdAsync(int eventId, CancellationToken token)
    {
        var participants = await _eventRepo.GetParticipantsByEventId(eventId, token);
        return participants.ToDtoList();
    }

    public async Task AddParticipantRatingUpdateAsync(AddParticipantRatingUpdateDto dto, CancellationToken token)
    {
        var @event = await _eventRepo.GetById(dto.EventId, token);
        if (@event is null) return;
        var entity = dto.ToRatingUpdate();
        await _ratingsProcessingService.Process(dto.ToRatingUpdate(), token);
    }

    public async Task AddEventRatingUpdateAsync(AddEventRatingUpdateDto dto, CancellationToken token)
    {
        var @event = await _eventRepo.GetById(dto.EventId, token);
        if (@event is null) return;
        var entity = dto.ToRatingUpdate();
        await _ratingsProcessingService.Process(dto.ToRatingUpdate(), token);
    }

    public async Task RemoveParticipantByEventIdAsync(int eventId, int participantId, CancellationToken token)
    {
        await _eventRepo.RemoveParticipantByEventId(eventId, participantId, token);
    }
}
