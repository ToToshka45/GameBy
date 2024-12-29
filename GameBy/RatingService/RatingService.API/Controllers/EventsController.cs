using Microsoft.AspNetCore.Mvc;
using RatingService.API.Configurations.Mappings;
using RatingService.API.Models.Events;
using RatingService.API.Models;
using RatingService.Domain.Entities;
using RatingService.Application.Services.Abstractions;
using RatingService.API.Models.Ratings;

namespace RatingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventLifecycleService _service;

        public EventsController(IEventLifecycleService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(IActionResult), 201)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<IActionResult> CreateEvent(CreateEventRequest req, CancellationToken token)
        {
            var result = await _service.AddNewEventAsync(req.ToDto(), token);
            if (result == null) { return BadRequest(); }
            return CreatedAtAction(nameof(GetEventById), new { id = result.Id }, result);
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        public async Task<ActionResult<GetEventResponse>> GetEvents(CancellationToken token)
        {
            var events = await _service.GetEventsAsync(token);
            return Ok(events.ToResponseList());
        }

        [HttpGet("get/{id:int}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<ActionResult<GetEventResponse>> GetEventById(int id, CancellationToken token)
        {
            var eventInfo = await _service.GetEventByIdAsync(id, token);
            if (eventInfo is null) { return NotFound(); }
            return Ok(eventInfo.ToResponse());
        }

        [HttpPost("{eventId:int}/set-rating")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        public async Task<IActionResult> SetEventRating(int eventId, AddEventRatingUpdateRequest req, CancellationToken token)
        {
            var dto = req.ToDto();
            dto.EventId = eventId;
            await _service.AddEventRatingUpdateAsync(dto, token);
            return Ok();
        }

        /// <summary>
        /// Finalizes an event, setting the correct state and updating the info about registered participants.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("{eventId:int}/finalize")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<IActionResult> FinalizeEvent(int eventId, FinalizeEventRequest req, CancellationToken token)
        {
            // TODO: create a domain event
            await _service.FinalizeEventAsync(req.ToDto(), token);
            return Ok();
        }

        // Participants

        [HttpPost("{eventId:int}/participants/add")]
        [ProducesResponseType(typeof(IActionResult), 201)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<IActionResult> AddParticipant(int eventId, AddParticipantRequest req, CancellationToken token)
        {
            // TODO: create a domain event
            var dto = req.ToDto(eventId);
            var result = await _service.AddParticipantAsync(eventId, dto, token);
            if (result is null) { return BadRequest(); }
            return CreatedAtAction(nameof(GetParticipantByEventId), new { eventId, participantId = result.Id }, result.ToResponse());
        }

        [HttpGet("{eventId:int}/participants/{participantId:int}")]
        [ProducesResponseType(typeof(ActionResult<Participant?>), 200)]
        public async Task<ActionResult<GetParticipantResponse?>> GetParticipantByEventId(int eventId, int participantId, CancellationToken token)
        {
            var participant = await _service.GetParticipantByEventIdAsync(eventId, participantId, token);
            if (participant is null) { return BadRequest(); }
            return Ok(participant.ToResponse());
        }

        [HttpGet("{eventId:int}/participants/get-all")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        public async Task<ActionResult<IEnumerable<GetParticipantResponse>>> GetParticipantsByEventId(int eventId, CancellationToken token)
        {
            var result = await _service.GetParticipantsByEventIdAsync(eventId, token);
            return Ok(result.ToResponseList());
        }

        [HttpDelete("{eventId:int}/participants/{participantId:int}/remove")]
        [ProducesResponseType(typeof(IActionResult), 204)]
        public async Task<IActionResult> RemoveParticipantByEventId(int eventId, int participantId, CancellationToken token)
        {
            await _service.RemoveParticipantByEventIdAsync(eventId, participantId, token);
            return NoContent();
        }

        // ratings

        [HttpPost("{eventId:int}/participants/{participantId:int}/set-rating")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        public async Task<IActionResult> SetParticipantRating(int eventId, int participantId, 
            AddParticipantRatingUpdateRequest req, CancellationToken token)
        {
            var dto = req.ToDto();
            dto.EventId = eventId;
            dto.ReceipientId = participantId;

            await _service.AddParticipantRatingUpdateAsync(dto, token);
            return Ok();
        }
    }
}
