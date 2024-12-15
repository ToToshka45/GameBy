using AutoMapper;
using GamerProfileService.Models;
using Gb.Gps.Services.Abstractions;
using Gb.Gps.Services.Contracts;
using Gb.Gps.WebHost.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.Gamer;

namespace GamerProfileService.Controllers;

/// <summary>
/// Игроки
/// </summary>
[ApiController]
[Route( "api/v1/[controller]" )]
public class GamerController : ControllerBase
{
    private readonly ILogger<GamerController> _logger;

    private readonly IGamerService _gamerService;
    private readonly IRankService _rankService;
    private readonly IMapper _mapper;

    public GamerController( ILogger<GamerController> logger, IGamerService gamerService, IRankService rankService, IMapper mapper )
    {
        _logger = logger;

        _gamerService = gamerService;
        _rankService = rankService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить данные всех игроков
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType<List<GamerModel>>( StatusCodes.Status200OK )]
    public async Task<List<GamerModel>> GetGamersAsync( CancellationToken cancellationToken )
    {
        var gamers = await _gamerService.GetAllAsync( cancellationToken );

        return _mapper.Map<List<GamerDto>, List<GamerModel>>( gamers );
    }

    /// <summary>
    /// Получить данные игрока по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet( "{id}" )]
    [ProducesResponseType<string>( StatusCodes.Status404NotFound )]
    [ProducesResponseType<GamerModel>( StatusCodes.Status200OK )]
    public async Task<ActionResult<GamerModel>> GetAsync( int id, CancellationToken cancellationToken )
    {
        var gamerDto = await _gamerService.GetByIdAsync( id, cancellationToken );

        return gamerDto == null ? NotFound( $"Игрок с id = {id} не найден" ) : Ok( _mapper.Map<GamerDto, GamerModel>( gamerDto ) );
    }

    /// <summary>
    /// Создать нового игрока
    /// </summary>
    /// <param name="createGamerModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType<int>( StatusCodes.Status201Created )]
    public async Task<ActionResult<int>> CreateGamerAsync( CreateGamerModel createGamerModel, CancellationToken cancellationToken )
    {
        var rankId = createGamerModel.RankId;
        var rankDto = await _rankService.GetByIdAsync( rankId, cancellationToken );

        if ( rankDto is null )
        {
            return NotFound( $"Звание с id = {rankId} не найдено" );
        }

        var result = await _gamerService.CreateAsync( _mapper.Map<CreateGamerModel, CreateGamerDto>( createGamerModel ), cancellationToken );

        return Created( string.Empty, result ); // TODO Anton: CreatedAtAction
    }

    /// <summary>
    /// Редактировать игрока
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateGamerModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut( "{id}" )]
    [ProducesResponseType<string>( StatusCodes.Status404NotFound )]
    [ProducesResponseType( StatusCodes.Status204NoContent )]
    public async Task<IActionResult> EditGamerAsync( int id, UpdateGamerModel updateGamerModel, CancellationToken cancellationToken )
    {
        var rankId = updateGamerModel.RankId;
        var rankDto = await _rankService.GetByIdAsync( rankId, cancellationToken );

        if ( rankDto is null )
        {
            return NotFound( $"Звание с id = {rankId} не найдено" );
        }

        var wasUpdated = await _gamerService.UpdateAsync( id, _mapper.Map<UpdateGamerModel, UpdateGamerDto>( updateGamerModel ), cancellationToken );

        return wasUpdated ? NoContent() : NotFound( $"Игрок с id = {id} не найден" );
    }

    /// <summary>
    /// Удалить игрока по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType<string>( StatusCodes.Status404NotFound )]
    [ProducesResponseType( StatusCodes.Status204NoContent )]
    public async Task<IActionResult> DeleteGamerAsync( int id, CancellationToken cancellationToken )
    {
        var wasDeleted = await _gamerService.DeleteAsync( id, cancellationToken );

        return wasDeleted ? NoContent() : NotFound( $"Игрок с id = {id} не найден" );
    }


}
