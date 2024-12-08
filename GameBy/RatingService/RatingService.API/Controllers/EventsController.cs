using Microsoft.AspNetCore.Mvc;
using RatingService.API.Models;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Abstractions;
using RatingService.Application.Abstractions;

namespace RatingService.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
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
        public async Task CreateEvent([FromBody] CreateEventRequest req)
        {
            
        }
    }
}
