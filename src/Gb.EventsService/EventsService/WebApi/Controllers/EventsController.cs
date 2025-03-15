﻿using Application;
using Application.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly EventService _eventService;

        private readonly IMapper _mapper;
        public EventsController(EventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        /// <summary>
        /// Создать мероприятие
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpPost("create")]
        public async Task<ActionResult<CreateEventResponse>> CreateEventAsync(CreateEventRequest request)
        {
            var eventId = await _eventService.CreateEvent(_mapper.Map<CreateEventDto>(request));

            if (eventId is null)
                return BadRequest();

            return Ok(new CreateEventResponse(eventId.Value));
        }

        /// <summary>
        /// Добавить участника
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpPost("{eventId:int}/participants/add")]
        public async Task<ActionResult<AddParticipantResponse>> AddParticipantAsync(AddParticipantRequest request)
        {
            var res = await _eventService.
                AddParticipant(_mapper.Map<ParticipantAddDto>(request));

            if (!res.IsSuccess)
                return BadRequest(res.ErrMessage);

            return _mapper.Map<AddParticipantResponse>(res);
        }

        /// <summary>
        /// Удалить участника
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpPost("{eventId:int}/participants/remove")]
        public async Task<ActionResult<bool>> RemovePlayerAsync(int eventId, RemoveParticipantRequest request)
        {
            bool res = await _eventService.
                PlayerRemove(new PlayerRemoveDto() { UserId = request.UserId, EventId = eventId });

            if (!res)
                return BadRequest();

            return true;
        }

        // note: тут мне кажется лучше сделать универсальный метод смены состояния участника, типа ChangeParticipantState

        /// <summary>
        /// Отметить участника как непришедшего
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpPost("{eventId:int}/participants/absent")]
        public async Task<ActionResult<bool>> SetPlayerAbsentAsync(int eventId, AddParticipantRequest request)
        {
            bool res = await _eventService.
                SetParticipantAbsent(new PlayerRemoveDto() { UserId = request.UserId, EventId = request.EventId });

            if (!res)
                return BadRequest();

            return true;
        }

        /// <summary>
        /// Отменить мероприятие
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpPost("{eventId:int}/cancel")]
        public async Task<ActionResult<bool>> CancelEventAsync(int eventId)
        {
            bool res = await _eventService.CancelEventAsync(eventId);

            if (!res)
                return BadRequest();

            return true;
        }

        /// <summary>
        /// Завершить мероприятие
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpGet("{eventId:int}/finish")]
        public async Task<ActionResult<bool>> FinishEventAsync(int eventId)
        {
            var res = await _eventService.FinishEventAsync(eventId);

            if (res is null)
                return BadRequest();

            return true;
        }

        /// <summary>
        /// Изменить мероприятие
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpPost("{eventId:int}/update")]
        public async Task<ActionResult<CreateEventResponse>> UpdateEventAsync(int eventId, UpdateEventRequest updateEventRequest)
        {
            var res = await _eventService.UpdateEvent(eventId, _mapper.Map<CreateEventDto>(updateEventRequest));

            if (res is null)
                return BadRequest();

            return Ok(_mapper.Map<CreateEventResponse>(res));
        }

        /// <summary>
        /// Получить все данные по мероприятию
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpGet("{eventId:int}")]
        public async Task<ActionResult<CreateEventDto>> GetEventAsync(int eventId)
        {
            var res = await _eventService.GetEvent(eventId);

            if (res is null)
                return BadRequest();

            return Ok(res);
        }

        /// <summary>
        /// Все мероприятия + фильтры
        /// </summary>
        /// <returns>
        /// Events if success or BadRequest 
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<List<GetEventResponse>>> GetEvents(EventsFilters filters)
        {
            var dto = _mapper.Map<EventsFiltersDto>(filters);
            var res = await _eventService.GetEvents(dto);
            return Ok(res);
        }

        /// <summary>
        /// Поиск мероприятий
        /// </summary>
        /// <returns>
        /// Events if success or BadRequest 
        /// </returns>
        //[HttpPost("filter")]
        //public async Task<ActionResult<List<GetEventsResponse>>> SearchEventsAsync(EventsFilters searchEventRequest)
        //{
        //    var res = await _eventService.GetEvents(_mapper.Map<EventsFilterDto>(searchEventRequest));

        //    if (res is null)
        //        return BadRequest();

        //    return Ok(res);
        //}
    }
}
