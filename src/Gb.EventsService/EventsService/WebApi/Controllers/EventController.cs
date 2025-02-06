using Application;
using Application.Dto;
using AutoMapper;
using Common;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using WebApi.Dto;

namespace WebApi.Controllers
{
    public class EventController : Controller
    {
        private readonly EventService _eventService;

        private readonly IMapper _mapper;
        public EventController(EventService eventService,IMapper mapper) {
            _eventService=eventService;
            _mapper=mapper;

        }

        /// <summary>
        /// Создать мероприятие
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpPost("CreateEvent")]
        public async Task<ActionResult<NewEventResponse>> CreateCustomerAsync(NewEventRequest request)
        {

            EventDto res = await _eventService.
                CreateNew(_mapper.Map<EventDto>(request));

            if (!res.IsSuccess)
                return BadRequest(res.ErrMessage);

            return _mapper.Map<NewEventResponse>(res);

        }

        /// <summary>
        /// Добавить участника
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpPost("AddPlayer")]
        public async Task<ActionResult<PlayerAddedResponse>> AddPlayerAsync(PlayerAddRequest request)
        {
            PlayerAddDto res = await _eventService.
                AddPlayer(_mapper.Map<PlayerAddDto>(request));

            if (!res.IsSuccess)
                return BadRequest(res.ErrMessage);

            return _mapper.Map<PlayerAddedResponse>(res);

        }

        /// <summary>
        /// Удалить участника
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpPost("RemovePlayer")]
        public async Task<ActionResult<bool>> RemovePlayerAsync(PlayerAddRequest request)
        {
            bool res = await _eventService.
                PlayerRemove(new PlayerRemoveDto() { UserId=request.UserId,EventId=request.EventId});

            if (!res)
                return BadRequest();

            return true;

        }

        /// <summary>
        /// Отметить участника как непришедшего
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpPost("SetPlayerAbsent")]
        public async Task<ActionResult<bool>> SetPlayerAbsent(PlayerAddRequest request)
        {
            bool res = await _eventService.
                SetPlayerIsAbsent(new PlayerRemoveDto() { UserId = request.UserId, EventId = request.EventId });

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
        [HttpPost("CancelEvent")]
        public async Task<ActionResult<bool>> RemoveEventAsync(int EventId)
        {
            bool res = await _eventService.EventRemove(EventId);

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
        [HttpGet("FinishEvent")]
        public async Task<ActionResult<bool>> FinishEventAsync(int EventId)
        {
            var res = await _eventService.EventFinish(EventId);

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
        [HttpPost("UpdateEvent")]
        public async Task<ActionResult<NewEventResponse>> UpdateEventAsync(UpdateEventRequest updateEvent)
        {

            var res = await _eventService.UpdateEvent(_mapper.Map<EventDto>(updateEvent));

            if (res is null)
                return BadRequest();
            else
                return _mapper.Map<NewEventResponse>(res);


        }

        /// <summary>
        /// Получить все данные по мероприятию
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpGet("GetEvent")]
        public async Task<ActionResult<EventDto>> GetEventAsync(int EventId)
        {
            var res = await _eventService.GetEvent(EventId);

            if (res is null)
                return BadRequest();
            else
                return res;


        }

        /// <summary>
        /// Поиск мероприятий
        /// </summary>
        /// <returns>
        /// Events if success or BadRequest 
        /// 
        /// </returns>
        [HttpPost("SearchEvents")]
        public async Task<ActionResult<List<ShortEventDto>>> SearchEventsAsync(EventsFilter searchEventRequest)
        {

            var res = await _eventService.GetEvents(_mapper.Map<EventsFilterDto>(searchEventRequest));

            if (res is null)
                return BadRequest();
            else
                return res;


        }

        /// <summary>
        /// Все мероприятия
        /// </summary>
        /// <returns>
        /// Events if success or BadRequest 
        /// 
        /// </returns>
        [HttpGet("GetAllEvents")]
        public async Task<ActionResult<List<ShortEventDto>>> GetAllEventsAsync(int UserId)
        {

            var res = await _eventService.GetAllEvents(UserId);

            if (res is null)
                return BadRequest();
            else
                return res;


        }
    }
}
