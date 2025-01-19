using Microsoft.AspNetCore.Mvc;
using RatingService.API.Configurations.Mappings;
using RatingService.API.Models.Users;
using RatingService.Application.Services.Abstractions;

namespace RatingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserLifecycleService _service;

        public UsersController(IUserLifecycleService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(IActionResult), 201)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest req, CancellationToken token)
        {
            var result = await _service.AddNewUserAsync(req.ToDto(), token);
            if (result is null) return BadRequest();
            return CreatedAtAction(nameof(GetUserInfo), new { id = result.Id }, result);
        }

        [HttpGet("get/{id:int}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<IActionResult> GetUserInfo(int id, CancellationToken token)
        {
            var user = await _service.GetUserInfoById(id, token);
            if (user == null) { return BadRequest(); }
            return Ok(user.ToResponse());
        }

        [HttpGet("get-users-info")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        public async Task<ActionResult<List<GetUserInfoResponse>>> GetUserInfo(CancellationToken token)
        {
            var userRatings = await _service.GetUsersInfo(token);
            return Ok(userRatings.ToResponseList());
        }

        [HttpGet("get-feedbacks/{id:int}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<ActionResult<GetUserRatingsResponse>> GetUserFeedbacks(int id, CancellationToken token)
        {
            var userInfo = await _service.GetUserFeedbacksAsync(id, token);
            if (userInfo is null) return NotFound();
            return Ok(userInfo.ToResponse());
        }

        //[HttpGet("{userId:int}/participations")]
        //[ProducesResponseType(typeof(IActionResult), 204)]
        //[ProducesResponseType(typeof(IActionResult), 400)]
        //public async Task<ActionResult<IEnumerable<UserParticipationResponse>>> GetUserParticipations(int userId, CancellationToken token)
        //{
        //    // TODO logic to get all the participantions of a particular User
        //    return NoContent();
        //}
    }
}
