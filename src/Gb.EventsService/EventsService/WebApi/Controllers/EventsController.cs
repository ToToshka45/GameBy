using Application;
using Application.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Minio.Exceptions;
using System.Net;
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
        public async Task<IActionResult> AddParticipantAsync(int eventId, AddParticipantRequest request)
        {
            var dto = _mapper.Map<AddParticipantDto>(request);
            dto.EventId = eventId;
            await _eventService.AddParticipant(dto);
            return Ok();
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

        [HttpGet("{eventId:int}/participants/{participantId:int}")]
        public async Task<IActionResult> SetParticipantState(int eventId, int participantId, [FromQuery] Common.ParticipationState state, [FromBody] DateTime? acceptedDate)
        {
            if (eventId == 0 || participantId == 0 || state is Common.ParticipationState.Unclarified) return BadRequest();
            await _eventService.UpdateParticipantState(eventId, participantId, state, acceptedDate);
            return Ok();
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
            bool res = await _eventService.SetParticipantAbsent(new PlayerRemoveDto() { UserId = request.UserId, EventId = eventId });

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
        /// Добавить тему мероприятия
        /// </summary>
        /// <returns>
        /// Event if success or BadRequest 
        /// InternalError Если не удалось добавить картинку
        /// </returns>
        //[HttpPost("{eventId:int}/add-image")]
        //public async Task<ActionResult<bool>> AddImageToEventAsync(int eventId, IFormFile file)
        //{
        //    var res = await _eventService.AddThemeToEventAsync(eventId, file);

        //    if (!res)
        //        return BadRequest();

        //    return true;
        //}

        /*
        [HttpGet("{eventId:int}/test-image")]
        public async Task<ActionResult> GetImageEventAsync(int eventId)
        {
            var fileStream = await _eventService.GetMediaTest(eventId);
        
            if (fileStream is null)
                return BadRequest();

            return File(fileStream, "application/octet-stream", "test."+);
        }*/

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
        [HttpPost("{eventId:int}")]
        public async Task<ActionResult<GetEventDto>> GetEventAsync(int eventId, EventFetchingParams parameters)
        {
            var res = await _eventService.GetEvent(eventId, parameters.UserId);

            if (res is null)
                return BadRequest();
            var response = _mapper.Map<GetEventResponse>(res);

            return Ok(response);
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
