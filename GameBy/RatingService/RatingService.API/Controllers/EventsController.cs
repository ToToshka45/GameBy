using Microsoft.AspNetCore.Mvc;
using RatingService.API.Models;
using RatingService.Application.Abstractions;
using RatingService.API.Configurations.Mappings;

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
        [ProducesResponseType(typeof(IActionResult), 400)]
        [ProducesResponseType(typeof(IActionResult), 500)]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest req, CancellationToken token)
        {
            var result = await _service.AddNewEventAsync(req.ToDto(), token);
            return CreatedAtAction(nameof(GetEventInfo), new { result.Id }, result);
        }

        [HttpGet("get-events")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        [ProducesResponseType(typeof(IActionResult), 500)]
        public async Task<ActionResult<GetEventInfoResponse>> GetEvents(CancellationToken token)
        {
            var events = await _service.GetEventsAsync(token);
            return Ok(events.ToShortResponseList());
        }

        [HttpGet("get-event-info/{id:int}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        [ProducesResponseType(typeof(IActionResult), 500)]
        public async Task<ActionResult<GetEventInfoResponse>> GetEventInfo(int id, CancellationToken token)
        {
            var eventInfo = await _service.GetEventInfoAsync(id, token);
            return Ok(eventInfo);
        }
    }
}
