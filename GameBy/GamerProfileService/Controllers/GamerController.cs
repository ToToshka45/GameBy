using AutoMapper;
using GamerProfileService.Models.Gamer;
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
    private readonly IGamerService _service;
    private readonly IMapper _mapper;

    public GamerController( IGamerService service, IMapper mapper )
    {
        _service = service;
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
        var gamers = await _service.GetAllAsync( cancellationToken );

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
        var gamerDto = await _service.GetByIdAsync( id, cancellationToken );

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
        var result = await _service.CreateAsync( _mapper.Map<CreateGamerModel, CreateGamerDto>( createGamerModel ), cancellationToken );

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
        var wasUpdated = await _service.UpdateAsync( id, _mapper.Map<UpdateGamerModel, UpdateGamerDto>( updateGamerModel ), cancellationToken );

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
        var wasDeleted = await _service.DeleteAsync( id, cancellationToken );

        return wasDeleted ? NoContent() : NotFound( $"Игрок с id = {id} не найден" );
    }
}
