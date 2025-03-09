using Application.Dto;
using AutoMapper;
using Common;
using Constants;
using DataAccess.Abstractions;
using Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application
{
    public class EventService
    {
        private readonly IRepository<Event> _eventRepository;

        private readonly IMapper _mapper;

        private readonly RabbitMqService _rabbitMqService;

        public EventService(IRepository<Event> eventRepository, IMapper mapper, RabbitMqService rabbitMqService)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _rabbitMqService = rabbitMqService;
        }

        public async Task<EventDto> CreateNew(EventDto eventDto)
        {
            eventDto.CreationDate=DateTime.Now;
            eventDto.EventStatus = Constants.EventStatus.Upcoming;
            var res=await _eventRepository.AddAsync(_mapper.Map<Event>(eventDto));
            if (res != null)
            {
                eventDto.IsSuccess = true;
                eventDto.Id = res.Id;
            }
                
            return eventDto;
        }

        public async Task<EventDto> GetEvent(int EventId,int UserId)
        {

            var Event = await _eventRepository.GetByIdAsync(EventId);
            if (Event is null)
            {
                return null;
            }
            EventDto res=_mapper.Map<EventDto>(Event);
            if(Event.EventMembers.Any(x=>x.UserId==UserId))
            {
                
                res.UserParticipationState=Event.EventMembers.First(x=>x.UserId==UserId).ParticipationState;
            }
            else if(Event.OrganizerId==UserId)
            {
                res.IsUserOrganizer=true;
            }

            res.PendingAcceptanceMemebers=Event.EventMembers.Where(x=>x.ParticipationState==ParticipationState.PendingAcceptance).ToList();
            res.AcceptedMemebers=Event.EventMembers.Where(x=>x.ParticipationState==ParticipationState.Registered).ToList();


            return res;
        }

        public async Task<List<ShortEventDto>> GetEvents(EventsFilterDto eventsFilterDto)
        {
            eventsFilterDto.SetUp();
            var Events = await _eventRepository.Search(x =>
        (string.IsNullOrEmpty(eventsFilterDto.EventTitle) || x.Title.ToLower().Contains(eventsFilterDto.EventTitle)) &&
        x.EventStatus==EventStatus.Upcoming &&
        (!eventsFilterDto.EventCategory.HasValue || x.EventCategory == eventsFilterDto.EventCategory) &&
        (!eventsFilterDto.FromDate.HasValue || x.EventDate >= eventsFilterDto.FromDate) &&
        (!eventsFilterDto.ToDate.HasValue || x.EventDate <= eventsFilterDto.ToDate));

            if (Events is null)
            {
                return null;
            }

            List<ShortEventDto> res= new List<ShortEventDto>();
            foreach (var Event in Events)
            {
                var ShortEvent = _mapper.Map<ShortEventDto>(Event);
                
                if (Event.EventMembers.Any(x => x.UserId == eventsFilterDto.UserId))
                { 
                    ShortEvent.IsUserParticipated=true;
                    ShortEvent.UserParticipationState=Event.EventMembers.First(x=>x.UserId==eventsFilterDto.UserId).ParticipationState;
                }
                if (Event.OrganizerId == eventsFilterDto.UserId)
                    ShortEvent.IsUserOrganizer = true;
                res.Add(ShortEvent);
            }

            return res;
        }

        public async Task<List<ShortEventDto>> GetAllEvents(int UserId)
        {

            var Events = await _eventRepository.GetAllAsync();
            if (Events is null)
            {
                return null;
            }

            List<ShortEventDto> res = new List<ShortEventDto>();
            foreach (var Event in Events)
            {
                var ShortEvent = _mapper.Map<ShortEventDto>(Event);
                
                if (Event.EventMembers.Any(x => x.UserId == UserId))
                { 
                    ShortEvent.IsUserParticipated=true;
                    ShortEvent.UserParticipationState=Event.EventMembers.First(x=>x.UserId==UserId).ParticipationState;
                }
                if(Event.OrganizerId==UserId)
                    ShortEvent.IsUserOrganizer=true;
                res.Add(ShortEvent);
            }

            return res;
        }

        public async Task<EventDto> UpdateEvent(EventDto eventDto)
        {
            var EventToUpd = await _eventRepository.GetByIdAsync(eventDto.Id);
            EventToUpd.EventDate = eventDto.EventDate;
            EventToUpd.EventStatus = eventDto.EventStatus;
            EventToUpd.ParticipantMinimum = eventDto.ParticipantMinimum;
            EventToUpd.MaxDuration = eventDto.MaxDuration;
            EventToUpd.Location = eventDto.Location;
            EventToUpd.Description = eventDto.Description;
            EventToUpd.Title = eventDto.Title;
            EventToUpd.ParticipantLimit = eventDto.ParticipantLimit;
            EventToUpd.ParticipantMinimum  =eventDto.ParticipantMinimum;
            var UpdatedEvent = await _eventRepository.UpdateAsync(EventToUpd);
            if (UpdatedEvent != null)
            {
                eventDto.IsSuccess = true;
            }

            return eventDto;
        }

        public async Task<PlayerAddDto> AddPlayer(PlayerAddDto addDto)
        {
            var res = addDto;

            var player = _mapper.Map<EventMember>(addDto);
            player.Role=Constants.EventUserRole.Player;
            player.ParticipationState=ParticipationState.PendingAcceptance;
            var EventToAdd = await _eventRepository.GetByIdAsync(addDto.EventId);
            player.EventId = EventToAdd.Id;
            EventToAdd.EventMembers.Add(player);

            EventAction eventActionPlayerAdded = new EventAction()
            {
                ParticipantId = player.UserId,
                EventId= addDto.EventId,
                EventType= Constants.EventType.PlayerAdded,
                PublicText="Игрок отправил запрос на участие"

            };
            EventToAdd.EventActions.Add(eventActionPlayerAdded);
            
            var UpdatedEvent=await _eventRepository.UpdateAsync(EventToAdd);
            
            if (UpdatedEvent != null)
            {
                res.IsSuccess = true;
                //ToDo
                res.Id = UpdatedEvent.EventMembers.FirstOrDefault(x=>x.UserId==addDto.UserId).Id;
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

            if(EventToDelete.EventStatus==EventStatus.Finished||EventToDelete.EventStatus==EventStatus.Canceled)
                return false;


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

        public async Task<bool> AcceptPlayer(PlayerAddDto playerRemove)
        {

            var EventToDelete = await _eventRepository.GetByIdAsync(playerRemove.EventId);

            if (EventToDelete is null)
            {
                return false;
            }

            if(EventToDelete.EventStatus==EventStatus.Finished)
            {
                return false;
            }

            var PlayerToRemove = EventToDelete.EventMembers.FirstOrDefault(x => x.UserId == playerRemove.UserId);
            if (PlayerToRemove is null)
                return false;

            
            PlayerToRemove.ParticipationState = ParticipationState.Registered;

            EventAction eventActionPlayerAbsent = new EventAction()
            {
                ParticipantId = playerRemove.UserId,
                EventId = playerRemove.EventId,
                EventType = Constants.EventType.PlayerAccepted,
                PublicText = "Игрок был принят"

            };
            EventToDelete.EventActions.Add(eventActionPlayerAbsent);


            await _eventRepository.UpdateAsync(EventToDelete);

            return true;
        }

        public async Task<bool> SetPlayerIsAbsent(PlayerRemoveDto playerRemove)
        {

            var EventToDelete = await _eventRepository.GetByIdAsync(playerRemove.EventId);

            if (EventToDelete is null)
            {
                return false;
            }

            if(EventToDelete.EventStatus!=EventStatus.Finished)
            {
                return false;
            }

            var PlayerToRemove = EventToDelete.EventMembers.FirstOrDefault(x => x.UserId == playerRemove.UserId);
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
            EventToDelete.EventActions.Add(eventActionPlayerAbsent);


            await _eventRepository.UpdateAsync(EventToDelete);

            return true;
        }

        public async Task<bool> EventRemove(int EventId)
        {

            var EventToDelete = await _eventRepository.GetByIdAsync(EventId);

            if (EventToDelete is null) 
            {
                return false;
            }

            if(EventToDelete.EventDate>DateTime.Now) return false;

            return await _eventRepository.DeleteAsync(EventToDelete);
        }

        public async Task<FinalizeEventRequest> EventFinish(int EventId)
        {

            var eventToFinsish = await _eventRepository.GetByIdAsync(EventId);

            if (eventToFinsish.EventDate > DateTime.Now) { 
                eventToFinsish.EventDate = DateTime.Now.ToUniversalTime();
               
            }
            eventToFinsish.EventStatus = EventStatus.Finished;
            EventAction eventAction = new EventAction()
            {
                CreationDate = DateTime.Now,
                EventId = EventId,
                EventType = EventType.EventFinished,
                PublicText="Мероприятие завершено"
            };
            eventToFinsish.EventActions.Add(eventAction);

            foreach(EventMember member in eventToFinsish.EventMembers.Where(x=>x.ParticipationState==ParticipationState.Registered))
            {
                EventAction eventmemberAction = new EventAction()
                {
                    CreationDate = DateTime.Now,
                    EventId = EventId,
                    ParticipantId=member.UserId,
                    EventType = member.IsAbsent?EventType.PlayerNotParticipated:EventType.PlayerParticipate,
                    PublicText = member.IsAbsent?"Игрок не принял участие":"Игрок поучаствовал"
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
            foreach (var eventMember in eventToFinsish.EventMembers.Where(x=>x.ParticipationState==ParticipationState.Registered))
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
