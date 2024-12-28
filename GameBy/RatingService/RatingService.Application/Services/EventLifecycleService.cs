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

    public async Task<int?> AddNewEventAsync(CreateEventDto newEvent, CancellationToken token)
    {
        try
        {
            var eventInfo = newEvent.ToEventInfo();
            var savedEvent = await _eventRepo.Add(eventInfo, token);
            return savedEvent.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while adding a new Event instance.");
            throw;
        }
    }

    public async Task<GetEventDto?> GetEventByIdAsync(int id, CancellationToken token)
    {
        try
        {
            //var eventInfo = await _eventRepo.GetById(id, token);
            var eventInfo = await _eventRepo.GetEntityWithIncludesAsync(id, token, [e => e.Participants]);
            if (eventInfo == null) { return null; }
            return eventInfo.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while retrieveing a stored Event instance.");
            throw;
        }
    }

    public async Task<ICollection<GetEventDto>> GetEventsAsync(CancellationToken token)
    {
        var events = await _eventRepo.GetAll(token);
        return events.ToDtoList();
    }

    public Task FinalizeEventAsync(FinalizeEventDto dto, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    // Participants 

    public async Task<int?> AddParticipantAsync(int eventId, AddParticipantDto dto, CancellationToken token)
    {
        try
        {
            var storedEvent = await _eventRepo.GetEntityWithIncludesAsync(eventId, token, [e => e!.Participants]);
            //var storedEvent = await _eventRepo.GetById(eventId, token, false);
            if (storedEvent is null) return null;

            storedEvent.ValidateParticipant(dto.ExternalParticipantId);
            var participant = dto.ToParticipant();
            storedEvent.AddParticipant(participant);
            await _eventRepo.SaveChangesAsync(token);
            //await _eventRepo.AddParticipant(storedEvent, participant, token);

            participant = await _eventRepo.GetParticipantByEventId(eventId, dto.ExternalParticipantId, token);
            return participant is null ? null : participant.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while trying to add a new Participant to an Event.");
            throw;
        }
    }

    public async Task<Participant?> GetParticipantByEventIdAsync(int eventId, int participantId, CancellationToken token)
    {
        return await _eventRepo.GetParticipantByEventId(eventId, participantId, token);
    }

    public async Task<IEnumerable<Participant>> GetParticipantsByEventIdAsync(int eventId, CancellationToken token)
    {
        return await _eventRepo.GetParticipantsByEventId(eventId, token);
    }

    public async Task AddParticipantRatingUpdate(AddParticipantRatingUpdateDto dto, CancellationToken token)
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
