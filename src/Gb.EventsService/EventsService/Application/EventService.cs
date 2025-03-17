using Application.Dto;
using AutoMapper;
using Common;
using Constants;
using DataAccess;
using DataAccess.Abstractions;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using WebApi.Dto;

namespace Application;

public class EventService
{
    private readonly IRepository<Event> _eventRepository;
    private readonly DbSet<Event> _events;

    private readonly IMapper _mapper;

    private readonly RabbitMqService _rabbitMqService;

    private readonly ILogger<EventService> _logger;

    public EventService(IRepository<Event> eventRepository, DataContext dbContext, IMapper mapper, RabbitMqService rabbitMqService, ILogger<EventService> logger)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _rabbitMqService = rabbitMqService;
        _events = dbContext.Set<Event>();
        _logger = logger;
    }

    public async Task<int?> CreateEvent(CreateEventDto eventDto)
    {
        try
        {
            // note: check an OrganizerId to exist in the DB

            //var dto = _mapper.Map<Event>(eventDto);
            var newEvent = new Event()
            {
                OrganizerId = eventDto.OrganizerId,
                Title = eventDto.Title,
                Description = eventDto.Description,
                Location = eventDto.Location,
                CreationDate = eventDto.CreationDate,
                EventDate = eventDto.EventDate,
                EventCategory = eventDto.EventCategory,
                EventStatus = Constants.EventStatus.Announced,
                MaxParticipants = eventDto.MaxParticipants,
                MinParticipants = eventDto.MinParticipants,
            };
            var res = await _eventRepository.AddAsync(newEvent);
            return res is not null ? res.Id : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while saving a new event");
            throw;
        }
    }

    public async Task<CreateEventDto?> GetEvent(int EventId)
    {
        var @event = await _eventRepository.GetByIdAsync(EventId);
        return @event is not null ? _mapper.Map<CreateEventDto>(@event) : null;
    }

    public async Task<List<GetShortEventDto>> GetEvents(EventsFiltersDto filters)
    {
        var query = _events
            .Where(e => e.EventDate >= filters.AfterDate && e.EventDate < filters.BeforeDate); 

        if (filters.EventCategories?.Length > 0)
        {
            query = query.Where(e => filters.EventCategories.Contains(e.EventCategory));
        }
        if (!string.IsNullOrWhiteSpace(filters.Title))
        {
            query = query.Where(e => e.Title.Trim().ToLower().Contains(filters.Title.Trim().ToLower()));
        }

        // TODO: pagination ??

        var events = await query
            .OrderBy(x => x.EventDate)
            .ToListAsync();

        return _mapper.Map<List<GetShortEventDto>>(events);
    }

    public async Task<CreateEventDto?> UpdateEvent(int eventId, CreateEventDto eventDto)
    {
        var eventToUpd = await _eventRepository.GetByIdAsync(eventId);
        if (eventToUpd == null) { return null; }

        eventToUpd.EventDate = eventDto.EventDate;
        eventToUpd.EventStatus = eventDto.EventStatus;
        eventToUpd.MinParticipants = eventDto.MinParticipants;
        //eventToUpd.MaxDuration = eventDto.MaxDuration;
        eventToUpd.Location = eventDto.Location;
        eventToUpd.Description = eventDto.Description;
        eventToUpd.Title = eventDto.Title;
        eventToUpd.MaxParticipants = eventDto.MaxParticipants;
        eventToUpd.MinParticipants = eventDto.MinParticipants;
        var updatedEvent = await _eventRepository.UpdateAsync(eventToUpd);

        return eventDto;
    }

    public async Task<ParticipantAddDto> AddParticipant(ParticipantAddDto addDto)
    {
        var res = addDto;

        var player = _mapper.Map<Participant>(addDto);
        player.Role = Constants.EventUserRole.Player;
        var eventToAdd = await _eventRepository.GetByIdAsync(addDto.EventId);
        player.EventId = eventToAdd.Id;
        eventToAdd.Participants.Add(player);

        EventAction eventActionPlayerAdded = new EventAction()
        {
            ParticipantId = player.UserId,
            EventId = addDto.EventId,
            EventType = Constants.EventType.PlayerAdded,
            PublicText = "Игрок принял участие"

        };
        eventToAdd.EventActions.Add(eventActionPlayerAdded);

        var updatedEvent = await _eventRepository.UpdateAsync(eventToAdd);

        if (updatedEvent != null)
        {
            res.IsSuccess = true;
            //ToDo
            res.Id = updatedEvent.Participants.FirstOrDefault(x => x.UserId == addDto.UserId).Id;
        }

        return res;
    }

    public async Task<bool> PlayerRemove(PlayerRemoveDto playerRemove)
    {
        var EventToDelete = await _eventRepository.GetByIdAsync(playerRemove.EventId);

        if (EventToDelete is null)
        {
            return false;
        }

        var PlayerToRemove = EventToDelete.Participants.FirstOrDefault(x => x.UserId == playerRemove.UserId);
        if (PlayerToRemove is null)
            return false;

        PlayerToRemove.LeaveDate = DateTime.Now;
        EventToDelete.Participants.Remove(PlayerToRemove);

        EventAction eventActionPlayerRemoved = new EventAction()
        {
            ParticipantId = playerRemove.UserId,
            EventId = playerRemove.EventId,
            EventType = Constants.EventType.PlayerRemoved,
            PublicText = "Игрок вышел"

        };
        EventToDelete.EventActions.Add(eventActionPlayerRemoved);

        await _eventRepository.UpdateAsync(EventToDelete);

        return true;
    }

    public async Task<bool> SetParticipantAbsent(PlayerRemoveDto playerRemove)
    {
        var eventToDelete = await _eventRepository.GetByIdAsync(playerRemove.EventId);

        if (eventToDelete is null)
        {
            return false;
        }

        if (eventToDelete.EventStatus != EventStatus.Finished)
        {
            return false;
        }

        var PlayerToRemove = eventToDelete.Participants.FirstOrDefault(x => x.UserId == playerRemove.UserId);
        if (PlayerToRemove is null)
            return false;

        PlayerToRemove.LeaveDate = DateTime.Now;
        PlayerToRemove.IsAbsent = true;

        EventAction eventActionPlayerAbsent = new EventAction()
        {
            ParticipantId = playerRemove.UserId,
            EventId = playerRemove.EventId,
            EventType = Constants.EventType.PlayerNotParticipated,
            PublicText = "Игрок не принял участие"

        };
        eventToDelete.EventActions.Add(eventActionPlayerAbsent);

        await _eventRepository.UpdateAsync(eventToDelete);

        return true;
    }

    public async Task<bool> CancelEventAsync(int EventId)
    {
        var eventToDelete = await _eventRepository.GetByIdAsync(EventId);

        if (eventToDelete is null)
        {
            return false;
        }

        if (eventToDelete.EventDate > DateTime.Now) return false;

        return await _eventRepository.DeleteAsync(eventToDelete);
    }

    public async Task<FinalizeEventRequest> FinishEventAsync(int EventId)
    {
        var eventToFinsish = await _eventRepository.GetByIdAsync(EventId);

        if (eventToFinsish.EventDate > DateTime.Now)
        {
            eventToFinsish.EventDate = DateTime.Now.ToUniversalTime();

        }
        eventToFinsish.EventStatus = EventStatus.Finished;
        EventAction eventAction = new EventAction()
        {
            CreationDate = DateTime.Now,
            EventId = EventId,
            EventType = EventType.EventFinished,
            PublicText = "Мероприятие завершено"
        };
        eventToFinsish.EventActions.Add(eventAction);

        foreach (Participant member in eventToFinsish.Participants)
        {
            EventAction eventmemberAction = new EventAction()
            {
                CreationDate = DateTime.Now,
                EventId = EventId,
                ParticipantId = member.UserId,
                EventType = member.IsAbsent ? EventType.PlayerNotParticipated : EventType.PlayerParticipate,
                PublicText = member.IsAbsent ? "Игрок не принял участие" : "Игрок поучаствовал"
            };
            eventToFinsish.EventActions.Add(eventmemberAction);
        }

        await _eventRepository.UpdateAsync(eventToFinsish);

        FinalizeEventRequest res = new FinalizeEventRequest();
        res.Category = eventToFinsish.EventCategory;
        res.State = EventProgressionState.Completed;
        res.CreationDate = eventToFinsish.CreationDate;
        res.FinishedDate = DateTime.Now;
        res.OrganizerId = eventToFinsish.OrganizerId;
        res.Title = eventToFinsish.Title;

        List<AddParticipantRequest> participantRequests = new List<AddParticipantRequest>();
        foreach (var eventMember in eventToFinsish.Participants)
        {
            AddParticipantRequest participantRequest = new AddParticipantRequest();
            participantRequest.ExternalParticipantId = eventMember.Id;
            participantRequest.ExternalUserId = eventMember.UserId;
            if (eventMember.IsAbsent)
                participantRequest.State = ParticipationState.Cancelled;
            else
                participantRequest.State = ParticipationState.Participated;
        }

        res.Participants = participantRequests;

        //rating_service_event_finished
        _rabbitMqService.SendMessage("rating_service_event_finished", JsonSerializer.Serialize(res));

        return res;
    }
}
