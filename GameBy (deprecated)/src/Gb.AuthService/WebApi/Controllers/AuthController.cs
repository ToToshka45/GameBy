﻿using Application;
using Application.Dto;
using WebApi.Dto;
using AutoMapper;
using DataAccess.Abstractions;
using Domain;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticantionService _authService;

        private readonly RegisterService _registerService;

        private readonly IMapper _mapper;

        public AuthController(RegisterService registerService, AuthenticantionService authService,
            IMapper mapper)
        {
            _authService = authService;

            _registerService = registerService;

            _mapper = mapper;
        }


        /// <summary>
        /// Логин по паролю и логину или email
        /// </summary>
        /// <returns>
        /// Unauthorized or LoginResultResponse
        /// </returns>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResultResponse>> Login(SimpleLoginDto request)
        {
            var res = await _authService.AuthUser(request.Password, request.Username, request.Email);

            if (!res.IsSuccess)
                return Unauthorized(res.ErrorMessage);

            return new LoginResultResponse()
            {
                AccessToken = res.AccessToken,
                RefreshToken = res.RefreshToken
            };
        }



        /// <summary>
        /// По refreshToken обновить токен
        /// </summary>
        /// <returns>
        /// Token Response or BadRequest 
        /// </returns>
        [HttpPost("RefreshTokens")]
        public async Task<ActionResult<LoginResultResponse>> RefreshTokens(string refreshToken)
        {
            //ToDo Хранить refresh token
            LoginResultResponse loginResultResponse = new LoginResultResponse();
            var res = await _authService.RefreshToken(refreshToken);
            if (res.IsSuccess)
            {
                loginResultResponse.AccessToken = res.AccessToken;
                loginResultResponse.RefreshToken = res.RefreshToken;
                return loginResultResponse;
            }

            return BadRequest(res.ErrorMessage);
        }

        /// <summary>
        /// По refreshToken обновить токен
        /// </summary>
        /// <returns>
        /// LoginInfo Response or BadRequest 
        /// </returns>
        [HttpPost("TokenInfo")]
        public async Task<ActionResult<int>> GetTokenInfo(string accessToken)
        {
            //ToDo Хранить refresh token
            LoginResultResponse loginResultResponse = new LoginResultResponse();
            var res =  _authService.GetTokenInfo(accessToken);
            if (res!=null)
            {
               
                return res;
            }

            return BadRequest();
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
