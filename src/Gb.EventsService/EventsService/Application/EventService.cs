using Application.Dto;
using AutoMapper;
using Common;
using Constants;
using DataAccess;
using DataAccess.Abstractions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Dto;
using Microsoft.AspNetCore.Http;

namespace Application
{
    public class EventService
    {
        private readonly IRepository<Event> _eventRepository;
        private readonly DbSet<Event> _events;

        private readonly IMapper _mapper;

        private readonly RabbitMqService _rabbitMqService;

        private readonly MinioService _minioService;

        public EventService(IRepository<Event> eventRepository, DataContext dbContext, IMapper mapper, 
        RabbitMqService rabbitMqService,MinioService minioService)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _rabbitMqService = rabbitMqService;
            _events = dbContext.Set<Event>();
            _minioService=minioService;
        }

        public async Task<int?> CreateEvent(CreateEventDto eventDto)
        {
            // note: check an OrganizerId to exist in the DB

            eventDto.CreationDate = DateTime.Now;
            eventDto.EventStatus = Constants.EventStatus.Announced;
            var res = await _eventRepository.AddAsync(_mapper.Map<Event>(eventDto));
            //if (res != null)
            //{
            //    eventDto.IsSuccess = true;
            //    eventDto.Id = res.Id;
            //}
            if (res is null) return null;
            return res.Id;
        }

        public async Task<CreateEventDto?> GetEvent(int EventId)
        {
            var @event = await _eventRepository.GetByIdAsync(EventId);
            if (@event is null)
            {
                return null;
            }
            var res=_mapper.Map<CreateEventDto>(@event);

            if(@event.ThemeId!=null)
            {
                var fileStream = await _minioService.DownloadFileAsync(@event.Id.ToString());
            
                if(fileStream!=null)
                {
                    var base64Content = Convert.ToBase64String(fileStream.ToArray());
                    res.ThemeFile=base64Content;
                    res.ThemeFileName="theme"+@event.ThemeExtension;
                }
            }
            
                

            return res;
        }

        public async Task<List<GetEventsDto>> GetEvents(EventsFiltersDto filters)
        {
            var query = _events
                .Where(e => e.EventDate >= filters.AfterDate && e.EventDate < filters.BeforeDate); 

            if (filters.EventCategories?.Length > 0)
            {
                query = query.Where(e => filters.EventCategories.Contains(e.EventCategory));
            }
            if (!string.IsNullOrWhiteSpace(filters.Title))
            {
                query = query.Where(e => e.Title == filters.Title);
            }

            // TODO: pagination ??

            var events = await query
                .OrderBy(x => x.EventDate)
                .ToListAsync();

            return _mapper.Map<List<GetEventsDto>>(events);
        }

        //public async Task<List<GetEventsDto>> GetEvents(int userId, EventsFiltersDto filters)
        //{
        //    var events = await _eventRepository.GetAllAsync();
        //    var res = new List<GetEventsDto>();
        //    foreach (var @event in events)
        //    {
        //        var shortEvent = _mapper.Map<GetEventsDto>(@event);
        //        //shortEvent.PlayerPlaces = $"{@event.EventMembers.Count} из {@event.ParticipantLimit}";
        //        if (@event.EventMembers.Any(x => x.UserId == userId))
        //            shortEvent.IsUserParticipant = true; 
        //        if (@event.OrganizerId == userId)
        //            shortEvent.IsUserOrganizer = true;
        //        res.Add(shortEvent);
        //    }

        //    // we return an empty collection or a filled one either way, so no need to explicitly return the empty array
        //    return res;
        //}

        public async Task<CreateEventDto> UpdateEvent(int eventId, CreateEventDto eventDto)
        {
            var eventToUpd = await _eventRepository.GetByIdAsync(eventId);
            eventToUpd.EventDate = eventDto.EventDate;
            eventToUpd.EventStatus = eventDto.EventStatus;
            eventToUpd.ParticipantMinimum = eventDto.ParticipantMinimum;
            eventToUpd.MaxDuration = eventDto.MaxDuration;
            eventToUpd.Location = eventDto.Location;
            eventToUpd.Description = eventDto.Description;
            eventToUpd.Title = eventDto.Title;
            eventToUpd.ParticipantLimit = eventDto.ParticipantLimit;
            eventToUpd.ParticipantMinimum = eventDto.ParticipantMinimum;
            var updatedEvent = await _eventRepository.UpdateAsync(eventToUpd);

            return eventDto;
        }

        public async Task<ParticipantAddDto> AddParticipant(ParticipantAddDto addDto)
        {
            var res = addDto;

            var player = _mapper.Map<Participant>(addDto);
            player.Role = Constants.EventUserRole.Player;
            var EventToAdd = await _eventRepository.GetByIdAsync(addDto.EventId);
            player.EventId = EventToAdd.Id;
            EventToAdd.EventMembers.Add(player);

            EventAction eventActionPlayerAdded = new EventAction()
            {
                ParticipantId = player.UserId,
                EventId = addDto.EventId,
                EventType = Constants.EventType.PlayerAdded,
                PublicText = "Игрок принял участие"

            };
            EventToAdd.EventActions.Add(eventActionPlayerAdded);

            var updatedEvent = await _eventRepository.UpdateAsync(EventToAdd);

            if (updatedEvent != null)
            {
                res.IsSuccess = true;
                //ToDo
                res.Id = updatedEvent.EventMembers.FirstOrDefault(x => x.UserId == addDto.UserId).Id;
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

            var PlayerToRemove = EventToDelete.EventMembers.FirstOrDefault(x => x.UserId == playerRemove.UserId);
            if (PlayerToRemove is null)
                return false;

            PlayerToRemove.LeaveDate = DateTime.Now;
            EventToDelete.EventMembers.Remove(PlayerToRemove);

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

            var PlayerToRemove = eventToDelete.EventMembers.FirstOrDefault(x => x.UserId == playerRemove.UserId);
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

        public async Task<bool> AddThemeToEventAsync(int EventId,IFormFile file)
        {
            var eventToAdd = await _eventRepository.GetByIdAsync(EventId);

            if (eventToAdd is null)
            {
                return false;
            }

            if (file == null || file.Length == 0)
                return false;
            
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif" };

    // Check if the extension is allowed
            if (!allowedExtensions.Contains(fileExtension))
            {
                return false;
                //return BadRequest("Invalid file extension. Allowed extensions are: .jpg, .jpeg, .png, .gif.");
            }

    // Check if the MIME type is allowed
            if (!allowedMimeTypes.Contains(file.ContentType.ToLowerInvariant()))
            {
                return false;
                //return BadRequest("Invalid file type. Allowed types are: image/jpeg, image/png, image/gif.");
            }
            //if (eventToAdd.EventDate > DateTime.Now) return false;
            await _minioService.UploadFileAsync(eventToAdd.Id.ToString(), file.OpenReadStream());
            eventToAdd.ThemeId=eventToAdd.Id;
            eventToAdd.ThemeExtension=fileExtension;
            await _eventRepository.UpdateAsync(eventToAdd);
            return true;
            //return await _eventRepository.DeleteAsync(eventToDelete);
        }

        public async Task<(Stream,string?)> GetMediaTest(int eventId)
        {
            var eventToAdd = await _eventRepository.GetByIdAsync(eventId);

            if (eventToAdd is null)
            {
                return (null,null);
            }
            var fileStream = await _minioService.DownloadFileAsync(eventId.ToString());
            return (fileStream,eventToAdd.ThemeExtension);
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

            foreach (Participant member in eventToFinsish.EventMembers)
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
            foreach (var eventMember in eventToFinsish.EventMembers)
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
}
