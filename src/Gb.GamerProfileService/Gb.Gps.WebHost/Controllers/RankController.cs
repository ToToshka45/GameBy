using AutoMapper;
using Gb.Gps.Services.Abstractions;
using Gb.Gps.Services.Contracts;
using Gb.Gps.WebHost.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gb.Gps.WebHost.Controllers
{

    /// <summary>
    /// Звания
    /// </summary>
    [ApiController]
    [Route( "api/v1/[controller]" )]
    public class RankController : ControllerBase
    {
        private readonly ILogger<RankController> _logger;

        private readonly IRankService _service;
        private readonly IMapper _mapper;

        public RankController( ILogger<RankController> logger, IRankService service, IMapper mapper )
        {
            _logger = logger;

            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить данные всех званий
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType<List<RankModel>>( StatusCodes.Status200OK )]
        public async Task<List<RankModel>> GetRanksAsync( CancellationToken cancellationToken )
        {
            var ranks = await _service.GetAllAsync( cancellationToken );

            return _mapper.Map<List<RankDto>, List<RankModel>>( ranks );
        }

        /// <summary>
        /// Получить данные звания по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet( "{id}" )]
        [ProducesResponseType<string>( StatusCodes.Status404NotFound )]
        [ProducesResponseType<RankModel>( StatusCodes.Status200OK )]
        public async Task<ActionResult<RankModel>> GetAsync( int id, CancellationToken cancellationToken )
        {
            var rankDto = await _service.GetByIdAsync( id, cancellationToken );

            return rankDto == null ? NotFound( $"Звание с id = {id} не найдено" ) : Ok( _mapper.Map<RankDto, RankModel>( rankDto ) );
        }

        /// <summary>
        /// Создать новое звание
        /// </summary>
        /// <param name="createRankModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType<int>( StatusCodes.Status201Created )]
        public async Task<ActionResult<int>> CreateRankAsync( CreateRankModel createRankModel, CancellationToken cancellationToken )
        {
            var result = await _service.CreateAsync( _mapper.Map<CreateRankModel, CreateRankDto>( createRankModel ), cancellationToken );

            return Created( string.Empty, result ); // TODO Anton: CreatedAtAction
        }

        /// <summary>
        /// Редактировать звание
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateRankModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut( "{id}" )]
        [ProducesResponseType<string>( StatusCodes.Status404NotFound )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        public async Task<IActionResult> EditRankAsync( int id, UpdateRankModel updateRankModel, CancellationToken cancellationToken )
        {
            var wasUpdated = await _service.UpdateAsync( id, _mapper.Map<UpdateRankModel, UpdateRankDto>( updateRankModel ), cancellationToken );

            return wasUpdated ? NoContent() : NotFound( $"Звание с id = {id} не найдено" );
        }

        /// <summary>
        /// Удалить звание по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType<string>( StatusCodes.Status404NotFound )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        public async Task<IActionResult> DeleteRankAsync( int id, CancellationToken cancellationToken )
        {
            var wasDeleted = await _service.DeleteAsync( id, cancellationToken );

            return wasDeleted ? NoContent() : NotFound( $"Звание с id = {id} не найдено" );
        }
    }
}
