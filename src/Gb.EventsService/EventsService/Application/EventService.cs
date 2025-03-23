using Application.Dto;
using AutoMapper;
using Common;
using Constants;
using DataAccess;
using DataAccess.Abstractions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application;

public class EventService
{
    private readonly IRepository<Event> _eventRepository;
    private readonly DbSet<Event> _events;

    private readonly IMapper _mapper;

    private readonly RabbitMqService _rabbitMqService;

    private readonly ILogger<EventService> _logger;
    private readonly MinioService _minioService;
    public EventService(IRepository<Event> eventRepository, DataContext dbContext, IMapper mapper, RabbitMqService rabbitMqService, ILogger<EventService> logger, MinioService minioService)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _rabbitMqService = rabbitMqService;
        _events = dbContext.Set<Event>();
        _logger = logger;
        _minioService = minioService;
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

    public async Task<GetEventDto?> GetEvent(int eventId, int? userId)
    {
        var @event = await _eventRepository.GetByIdAsync(eventId);
        if (@event == null) { return null; }

        var res = _mapper.Map<GetEventDto>(@event);
        if (userId.HasValue)
        {
            res.IsParticipant = @event.Participants.Any(p => p.UserId == userId);
            res.IsOrganizer = @event.OrganizerId == userId;
        }
        //if (@event.ThemeId != null)
        //{
        //    var fileStream = await _minioService.DownloadFileAsync(@event.Id.ToString());

        //    if (fileStream != null)
        //    {
        //        var base64Content = Convert.ToBase64String(fileStream.ToArray());
        //        res.ThemeFile = base64Content;
        //        res.ThemeFileName = "theme" + @event.ThemeExtension;
        //    }
        //}

        return res;
    }

    //public async Task<bool> AddThemeToEventAsync(int EventId, IFormFile file)
    //{
    //    var eventToAdd = await _eventRepository.GetByIdAsync(EventId);

    //    if (eventToAdd is null)
    //    {
    //        return false;
    //    }

    //    if (file == null || file.Length == 0)
    //        return false;

    //    var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

    //    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
    //    var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif" };

    //    // Check if the extension is allowed
    //    if (!allowedExtensions.Contains(fileExtension))
    //    {
    //        return false;
    //        //return BadRequest("Invalid file extension. Allowed extensions are: .jpg, .jpeg, .png, .gif.");
    //    }

    //    // Check if the MIME type is allowed
    //    if (!allowedMimeTypes.Contains(file.ContentType.ToLowerInvariant()))
    //    {
    //        return false;
    //        //return BadRequest("Invalid file type. Allowed types are: image/jpeg, image/png, image/gif.");
    //    }
    //    //if (eventToAdd.EventDate > DateTime.Now) return false;
    //    await _minioService.UploadFileAsync(eventToAdd.Id.ToString(), file.OpenReadStream());
    //    eventToAdd.ThemeId = eventToAdd.Id;
    //    eventToAdd.ThemeExtension = fileExtension;
    //    await _eventRepository.UpdateAsync(eventToAdd);
    //    return true;
    //    //return await _eventRepository.DeleteAsync(eventToDelete);
    //}

    //public async Task<(Stream?, string?)> GetMediaTest(int eventId)
    //{
    //    var eventToAdd = await _eventRepository.GetByIdAsync(eventId);

    //    if (eventToAdd is null)
    //    {
    //        return (null, null);
    //    }
    //    var fileStream = await _minioService.DownloadFileAsync(eventId.ToString());
    //    return (fileStream, eventToAdd.ThemeExtension);
    //}

    public async Task<List<GetShortEventDto>> GetEvents(EventsFiltersDto filters)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while retrieving events.");
            throw;
        }
    }

    public async Task<GetEventsByUserIdDto> GetUserEvents(int userId, DateTime currentTime)
    {
        try
        {
            GetEventsByUserIdDto dto = new();
            dto.UserId = userId;

            dto.GamerEvents = await _events
                .Where(e => e.EventDate >= currentTime && e.Participants.Any(p => p.UserId == userId))
                .Select(e => _mapper.Map<GetShortEventDto>(e))
                .ToListAsync();

            dto.OrganizerEvents = await _events
                .Where(e => e.EventDate >= currentTime && e.OrganizerId == userId)
                .Select(e => _mapper.Map<GetShortEventDto>(e))
                .ToListAsync();

            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while retrieving events.");
            throw;
        }
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

    public async Task AddParticipant(AddParticipantDto addDto)
    {
        var player = _mapper.Map<Participant>(addDto);
        player.ApplyDate = addDto.ApplyDate;
        var eventToAdd = await _eventRepository.GetByIdAsync(addDto.EventId);
        ArgumentNullException.ThrowIfNull(eventToAdd);
        eventToAdd.Participants.Add(player);

        //EventAction eventActionPlayerAdded = new EventAction()
        //{
        //    ParticipantId = player.UserId,
        //    EventId = addDto.EventId,
        //    EventType = Constants.EventType.PlayerAdded,
        //    PublicText = "Игрок принял участие"

        //};
        //eventToAdd.EventActions.Add(eventActionPlayerAdded);

        await _eventRepository.UpdateAsync(eventToAdd);

        //if (updatedEvent != null)
        //{
        //    res.IsSuccess = true;
        //    //ToDo
        //    res.Id = updatedEvent.Participants.FirstOrDefault(x => x.UserId == addDto.UserId).Id;
        //}

        //return res;
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
        //PlayerToRemove.IsAbsent = true;

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
        var eventToFinish = await _eventRepository.GetByIdAsync(EventId);

        if (eventToFinish.EventDate > DateTime.Now)
        {
            eventToFinish.EventDate = DateTime.Now.ToUniversalTime();

        }
        eventToFinish.EventStatus = EventStatus.Finished;
        EventAction eventAction = new EventAction()
        {
            CreationDate = DateTime.Now,
            EventId = EventId,
            EventType = EventType.EventFinished,
            PublicText = "Мероприятие завершено"
        };
        eventToFinish.EventActions.Add(eventAction);

        foreach (Participant member in eventToFinish.Participants)
        {
            EventAction eventmemberAction = new EventAction()
            {
                CreationDate = DateTime.Now,
                EventId = EventId,
                ParticipantId = member.UserId,
                //EventType = member.IsAbsent ? EventType.PlayerNotParticipated : EventType.PlayerParticipate,
                //PublicText = member.IsAbsent ? "Игрок не принял участие" : "Игрок поучаствовал"
            };
            eventToFinish.EventActions.Add(eventmemberAction);
        }

        await _eventRepository.UpdateAsync(eventToFinish);

        FinalizeEventRequest res = new FinalizeEventRequest();
        res.Category = eventToFinish.EventCategory;
        res.State = EventProgressionState.Completed;
        res.CreationDate = eventToFinish.CreationDate;
        res.FinishedDate = DateTime.Now;
        res.OrganizerId = eventToFinish.OrganizerId;
        res.Title = eventToFinish.Title;

        List<AddParticipantRequest> participantRequests = new List<AddParticipantRequest>();
        foreach (var eventMember in eventToFinish.Participants)
        {
            AddParticipantRequest participantRequest = new AddParticipantRequest();
            participantRequest.ExternalParticipantId = eventMember.Id;
            participantRequest.ExternalUserId = eventMember.UserId;
            //if (eventMember.IsAbsent)
            //    participantRequest.State = ParticipationState.Cancelled;
            //else
            //    participantRequest.State = ParticipationState.Participated;
        }

        res.Participants = participantRequests;

        //rating_service_event_finished
        _rabbitMqService.SendMessage("rating_service_event_finished", JsonSerializer.Serialize(res));

        return res;
    }

    public async Task UpdateParticipantState(int eventId, int participantId, ParticipationState state, DateTime? acceptedDate)
    {
        var @event = await _eventRepository.GetByIdAsync(eventId);
        if (@event is null) return;

        var participant = @event.Participants.FirstOrDefault(p => p.Id == participantId);
        if (participant is null) return;

        participant.State = state;
        if (acceptedDate.HasValue)
            participant.AcceptedDate = acceptedDate.Value;

        await _eventRepository.UpdateAsync(@event);
    }
}
