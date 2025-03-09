using Application;
using Application.Dto;
using AutoMapper;
using Common;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [HttpPost("сreate-event")]
        public async Task<ActionResult<int>> CreateCustomerAsync(NewEventRequest request)
        {

            EventDto res = await _eventService.
                CreateNew(_mapper.Map<EventDto>(request));

            if (!res.IsSuccess)
                return BadRequest(res.ErrMessage);

            return res.Id;

        }

        /// <summary>
        /// Добавить участника
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpPost("{eventId}/participants/add-player")]
        public async Task<ActionResult<PlayerAddedResponse>> AddPlayerAsync(int eventId,[FromBody]PlayerAddRequest request)
        {
            request.EventId=eventId;

            PlayerAddDto res = await _eventService.
                AddPlayer(_mapper.Map<PlayerAddDto>(request));

            if (!res.IsSuccess)
                return BadRequest(res.ErrMessage);

            return _mapper.Map<PlayerAddedResponse>(res);

        }

        // <summary>
        /// Принять участника
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpPost("{eventId}/participants/accept-player")]
        public async Task<ActionResult<bool>> AcceptPlayerAsync(int eventId,[FromBody]PlayerAddRequest request)
        {
            request.EventId=eventId;
            bool res = await _eventService.
                AcceptPlayer(_mapper.Map<PlayerAddDto>(request));

            if (!res)
                return BadRequest("Не удалось принять игрока");

            return true;

        }

        /// <summary>
        /// Удалить участника
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpPost("{eventId}/participants/remove-player")]
        public async Task<ActionResult<bool>> RemovePlayerAsync(int eventId,[FromBody]PlayerAddRequest request)
        {
            bool res = await _eventService.
                PlayerRemove(new PlayerRemoveDto() { UserId=request.UserId,EventId=eventId});

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
        [HttpPost("{eventId}/participants/set-player-absent")]
        public async Task<ActionResult<bool>> SetPlayerAbsent(int eventId,[FromBody]PlayerAddRequest request)
        {
            bool res = await _eventService.
                SetPlayerIsAbsent(new PlayerRemoveDto() { UserId = request.UserId, EventId = eventId });

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
        [HttpGet("{eventId}/cancel-event")]
        public async Task<ActionResult<bool>> RemoveEventAsync(int eventId)
        {
            bool res = await _eventService.EventRemove(eventId);

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
        [HttpGet("{eventId}/finish-event")]
        public async Task<ActionResult<bool>> FinishEventAsync(int eventId)
        {
            var res = await _eventService.EventFinish(eventId);

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
        [HttpPost("update-event")]
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
        [HttpGet("get-event")]
        public async Task<ActionResult<EventDto>> GetEventAsync(int EventId,int UserId)
        {
            var res = await _eventService.GetEvent(EventId,UserId);

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
        [HttpPost("search-events")]
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
        [HttpGet("get-all-events")]
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
