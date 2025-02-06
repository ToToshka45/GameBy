using Application;
using Application.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly RegisterService _registerService;

        private readonly IMapper _mapper;

        public RegisterController(RegisterService registerService, 
            IMapper mapper)
        {

            _registerService = registerService;

            _mapper = mapper;
        }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <returns>
        /// Customer if success or BadRequest 
        /// InternalError Если не удалось добавить пользователя но запрос валидацию прошёл
        /// </returns>
        [HttpPost("RegisterNew")]
        public async Task<ActionResult<NewUserResponse>> CreateCustomerAsync(RegiserUserRequest request)
        {
            NewUserResultDto res = await _registerService.
                AddNewUser(_mapper.Map<NewUserDto>(request));


            if (!res.IsSuccess)
                return BadRequest(res.ErrorMessage);

            

            return new NewUserResponse() { UserName = res.UserName, Id = res.Id };

        }

        /// <summary>
        /// Существует ли логин
        /// </summary>
        /// <returns>
        /// true if exists else false  
        /// </returns>
        [HttpGet("CheckLoginExists")]
        public async Task<ActionResult<bool>> CheckLoginExists(string login)
        {

            return await _registerService.CheckLoginExists(login);
        }

        /// <summary>
        /// Существует ли email
        /// </summary>
        /// <returns>
        /// true if exists else false  
        /// </returns>
        [HttpGet("CheckEmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string login)
        {

            return await _registerService.CheckEmailExists(login);
        }

    }
}
