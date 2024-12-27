using Microsoft.AspNetCore.Mvc;
using RatingService.API.Configurations.Mappings;
using RatingService.API.Models.Events;
using RatingService.API.Models;
using RatingService.Domain.Entities;
using RatingService.Application.Services.Abstractions;

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


        // Participants

        [HttpPost("{eventId:int}/participants/add")]
        [ProducesResponseType(typeof(IActionResult), 201)]
        public async Task<IActionResult> AddParticipant(int eventId, AddParticipantRequest req, CancellationToken token)
        {
            // TODO: create a domain event
            var result = await _service.AddParticipantAsync(eventId, req.ToDto(), token);
            return Created();
        }


        [HttpGet("{eventId:int}/participants/get-all")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        public async Task<ActionResult<Participant>> GetParticipantsByEventId(int eventId, CancellationToken token)
        {
            return Ok(await _service.GetParticipantsByEventIdAsync(eventId, token));
        }

    }
}
