using Application;
using WebApi.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticantionService _authService;
        private readonly RegisterService _registerService;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthController(RegisterService registerService, AuthenticantionService authService,
            IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _authService = authService;

            _registerService = registerService;

            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Логин по паролю и логину или email
        /// </summary>
        /// <returns>
        /// Unauthorized or LoginResultResponse
        /// </returns>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(SimpleLoginDto request)
        {
            var res = await _authService.AuthUser(request.Password, request.Username, request.Email);

            if (res is null)
                return Unauthorized();

            HttpContext.Response.Cookies.Append("refreshToken", res.RefreshToken, new()
            {
                Expires = JwtSettings.RefreshTokenExpiresAt,
                HttpOnly = true,
                IsEssential = true,
                SameSite = SameSiteMode.Lax,
                Secure = _jwtSettings.UseSslProtection
            });

            return Ok(new LoginResponse()
            {
                Id = res.Id,
                Username = res.Username,
                Email = res.Email,
                AccessToken = res.AccessToken,
                //RefreshToken = res.RefreshToken
            });
        }

        /// <summary>
        /// По refreshToken обновить токен
        /// </summary>
        /// <returns>
        /// Token Response or BadRequest 
        /// </returns>
        [HttpGet("refresh")]
        public async Task<ActionResult<LoginResponse>> RefreshTokens()
        {
            HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken);
            if (string.IsNullOrWhiteSpace(refreshToken)) return Unauthorized();
            //ToDo Хранить refresh token
            LoginResponse loginResultResponse = new();
            var res = await _authService.RefreshToken(refreshToken);

            if (!res.IsSuccess) return BadRequest(res.ErrorMessage);

            loginResultResponse.AccessToken = res.AccessToken;
            loginResultResponse.RefreshToken = res.RefreshToken;

            return Ok(loginResultResponse);
        }

        /// <summary>
        /// По refreshToken обновить токен
        /// </summary>
        /// <returns>
        /// LoginInfo Response or BadRequest 
        /// </returns>
        [HttpPost("validate-token")]
        public async Task<ActionResult<int>> GetTokenInfo(string accessToken)
        {
            //ToDo Хранить refresh token
            LoginResponse loginResultResponse = new LoginResponse();
            var res = _authService.GetTokenInfo(accessToken);
            if (res != null)
            {

                return res;
            }

            return StatusCode(403);
        }

        [HttpGet("About")]
        public IActionResult About()
        {
            return Ok();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }

    }
}
