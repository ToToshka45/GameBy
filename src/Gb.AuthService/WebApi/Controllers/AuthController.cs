using Application;
using WebApi.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var res = await _authService.AuthUser(request.Password, request.Username, request.Email);

            if (res is null)
                return Unauthorized();

            AppendRefreshToken(res.RefreshToken);

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
        public async Task<ActionResult<RefreshTokenResponse>> RefreshTokens()
        {
            HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken);
            if (string.IsNullOrWhiteSpace(refreshToken)) return Unauthorized();
            //ToDo Хранить refresh token
            var res = await _authService.RefreshToken(refreshToken);
            if (res is null)
            {
                Response.Cookies.Delete("refreshToken");
                //AppendRefreshToken("");
                return Unauthorized();
            }

            var response = _mapper.Map<RefreshTokenResponse>(res);
            AppendRefreshToken(res.RefreshToken);
            //loginResultResponse.RefreshToken = res.RefreshToken;

            return Ok(response);
        }

        /// <summary>
        /// По refreshToken обновить токен
        /// </summary>
        /// <returns>
        /// LoginInfo Response or BadRequest 
        /// </returns>
        [HttpGet("validate-token")]
        //[Authorize]
        public async Task<ActionResult<ValidateTokenResponse>> ValidateToken()
        {
            Request.Headers.TryGetValue("Authorization", out var value);
            var bearerToken = value.ToString();
            if (!bearerToken.StartsWith("Bearer ")) return Unauthorized(new { message = "Authorization header must start with 'Bearer'." });

            var token = bearerToken.Substring("Bearer ".Length).Trim();

            var res = await _authService.RestoreUserInfo(token);
            if (res is null)
            {
                return Unauthorized();
            }

            return Ok(new ValidateTokenResponse()
            {
                Id = res.Id,
                Username = res.Username,
                Email = res.Email,
                AccessToken = res.AccessToken
            });
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

        private void AppendRefreshToken(string refreshToken)
        {
            HttpContext.Response.Cookies.Append("refreshToken", refreshToken, new()
            {
                Expires = DateTime.UtcNow.AddMinutes(JwtSettings.RefreshTokenExpiresInMinutes),
                HttpOnly = true,
                IsEssential = true,
                SameSite = SameSiteMode.Strict,
                Secure = _jwtSettings.UseSslProtection
            });
        }
    }
}
