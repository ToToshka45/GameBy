using Microsoft.AspNetCore.Mvc;
using RatingService.API.Configurations.Mappings;
using RatingService.API.Models.Events;
using RatingService.API.Models.Users;
using RatingService.Application.Services.Abstractions;

namespace RatingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserLifecycleService _service;

        public UserController(IUserLifecycleService service)
        {
            _service = service;
        }

        [HttpPost("add-user")]
        [ProducesResponseType(typeof(IActionResult), 201)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest req, CancellationToken token)
        {
            var id = await _service.AddNewUserAsync(req.ToDto(), token);
            return CreatedAtAction(nameof(GetUser), new { id }, req);
        }

        [HttpGet("get-user/{userId:int}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<IActionResult> GetUser(int userId, CancellationToken token)
        {
            var user = await _service.GetUserById(userId, token);
            if (user == null) { return BadRequest(); }
            return Ok(user.ToResponse());
        }

        [HttpGet("get-user-ratings/{userId:int}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<IActionResult> GetUserRatings(int userId, CancellationToken token)
        {
            var userRatings = await _service.GetUserRatingsAsync(userId, token);
            if (userRatings == null) { return BadRequest(); }
            return Ok(userRatings.ToResponse());
        }

        [HttpGet("get-user-feedbacks/{id:int}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<ActionResult<GetUserRatingsResponse>> GetUserFeedbacks(int id, CancellationToken token)
        {
            var userInfo = await _service.GetUserFeedbacksAsync(id, token);
            if (userInfo is null) return NotFound();
            return Ok(userInfo.ToResponse());
        }

        [HttpGet("{userId:int}/participations")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<ActionResult<IEnumerable<UserParticipationResponse>>> GetUserParticipations(int userId, CancellationToken token)
        {
            // TODO logic to get all the participantions of a particular User
            return Ok();
        }

    }
}
