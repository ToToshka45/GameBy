using AutoMapper;
using GamerProfileService.Controllers;
using GamerProfileService.Models.Gamer;
using Gb.Gps.Services.Abstractions;
using Gb.Gps.Services.Contracts;
using Gb.Gps.WebHost.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.Gamer;

namespace Gb.Gps.WebHost.Controllers
{
    /// <summary>
    /// Достижения
    /// </summary>
    [ApiController]
    [Route( "api/v1/[controller]" )]
    public class AchievementController : ControllerBase
    {
        private readonly ILogger<AchievementController> _logger;

        private readonly IAchievementService _service;
        private readonly IMapper _mapper;

        public AchievementController( ILogger<AchievementController> logger, IAchievementService service, IMapper mapper )
        {
            _logger = logger;

            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить данные всех достижений
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType<List<AchievementModel>>( StatusCodes.Status200OK )]
        public async Task<List<AchievementModel>> GetGamersAsync( CancellationToken cancellationToken )
        {
            var achievements = await _service.GetAllAsync( cancellationToken );

            return _mapper.Map<List<AchievementDto>, List<AchievementModel>>( achievements );
        }

        /// <summary>
        /// Получить данные достижения по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet( "{id}" )]
        [ProducesResponseType<string>( StatusCodes.Status404NotFound )]
        [ProducesResponseType<AchievementModel>( StatusCodes.Status200OK )]
        public async Task<ActionResult<AchievementModel>> GetAsync( int id, CancellationToken cancellationToken )
        {
            var achievementDto = await _service.GetByIdAsync( id, cancellationToken );

            return achievementDto == null ? NotFound( $"Игрок с id = {id} не найден" ) : Ok( _mapper.Map<AchievementDto, AchievementModel>( achievementDto ) );
        }

        /// <summary>
        /// Создать новое достижение
        /// </summary>
        /// <param name="createAchievementModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType<int>( StatusCodes.Status201Created )]
        public async Task<ActionResult<int>> CreateGamerAsync( CreateAchievementModel createAchievementModel, CancellationToken cancellationToken )
        {
            var result = await _service.CreateAsync( _mapper.Map<CreateAchievementModel, CreateAchievementDto>( createAchievementModel ), cancellationToken );

            return Created( string.Empty, result ); // TODO Anton: CreatedAtAction
        }

        /// <summary>
        /// Редактировать достижение
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAchievementModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut( "{id}" )]
        [ProducesResponseType<string>( StatusCodes.Status404NotFound )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        public async Task<IActionResult> EditGamerAsync( int id, UpdateAchievementModel updateAchievementModel, CancellationToken cancellationToken )
        {
            var wasUpdated = await _service.UpdateAsync( id, _mapper.Map<UpdateAchievementModel, UpdateAchievementDto>( updateAchievementModel ), cancellationToken );

            return wasUpdated ? NoContent() : NotFound( $"Игрок с id = {id} не найден" );
        }

        /// <summary>
        /// Удалить достижение по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType<string>( StatusCodes.Status404NotFound )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        public async Task<IActionResult> DeleteGamerAsync( int id, CancellationToken cancellationToken )
        {
            var wasDeleted = await _service.DeleteAsync( id, cancellationToken );

            return wasDeleted ? NoContent() : NotFound( $"Игрок с id = {id} не найден" );
        }
    }
}
