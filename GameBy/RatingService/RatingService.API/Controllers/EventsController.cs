using Microsoft.AspNetCore.Mvc;
using RatingService.Application.Abstractions;
using RatingService.API.Configurations.Mappings;
using RatingService.API.Models.Events;
using RatingService.API.Models;

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

        [HttpPost("create-event")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest req, CancellationToken token)
        {
            var id = await _service.AddNewEventAsync(req.ToDto(), token);
            return CreatedAtAction(nameof(GetEventById), new { id }, req);
        }

        [HttpGet("get-events")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        public async Task<ActionResult<GetEventResponse>> GetEvents(CancellationToken token)
        {
            var events = await _service.GetEventsAsync(token);
            return Ok(events.ToResponseList());
        }

        [HttpGet("get-event-info/{id:int}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<ActionResult<GetEventResponse>> GetEventById(int id, CancellationToken token)
        {
            var eventInfo = await _service.GetEventByIdAsync(id, token);
            if (eventInfo is null) { return NotFound(); }
            return Ok(eventInfo.ToResponse());
        }

        // Participants

        [HttpPost("{eventId:int}/participants/add")]
        [ProducesResponseType(typeof(IActionResult), 201)]
        public async Task<IActionResult> AddParticipant(int eventId, AddParticipantRequest req, CancellationToken token)
        {
            // TODO: create a domain event
            await _service.AddParticipantAsync(eventId, req.ToDto(), token);
            return Created();
        }

        /// <summary>
        /// Finalizes an event, setting the correct state and updating the info about registered participants.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("finalize")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<IActionResult> FinalizeEvent(FinalizeEventRequest req, CancellationToken token)
        {
            // TODO: create a domain event
            await _service.FinalizeEventAsync(req.ToDto(), token);
            return Ok();
        }

    }
}
