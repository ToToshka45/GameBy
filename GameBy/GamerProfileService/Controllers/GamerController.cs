using AutoMapper;
using GamerProfileService.Models;
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
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType<IActionResult>( StatusCodes.Status200OK )]
    public async Task<ActionResult<List<GamerResponse>>> GetGamersAsync()
    {
        var gamers = await _service.GetAllAsync( Request.HttpContext.RequestAborted );

        return Ok( _mapper.Map<List<GamerDto>, List<GamerModel>>( gamers ) );
    }

    /// <summary>
    /// Получить данные игрока по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet( "{id}" )]
    [ProducesResponseType<IActionResult>( StatusCodes.Status404NotFound )]
    [ProducesResponseType<IActionResult>( StatusCodes.Status200OK )]
    public async Task<IActionResult> GetAsync( int id )
    {
        var gamerDto = await _service.GetByIdAsync( id, Request.HttpContext.RequestAborted );

        if ( gamerDto == null )
        {
            return NotFound( $"Игрок с id = {id} не найден" );
        }

        return Ok( _mapper.Map<GamerDto, GamerModel>( gamerDto ) );
    }

    /// <summary>
    /// Создать нового игрока
    /// </summary>
    /// <param name="createGamerModel"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType<IActionResult>( StatusCodes.Status201Created )]
    public async Task<IActionResult> CreateGamerAsync( CreateGamerModel createGamerModel )
    {
        var result = await _service.CreateAsync( _mapper.Map<CreateGamerModel, CreateGamerDto>( createGamerModel ), Request.HttpContext.RequestAborted );

        return Created( string.Empty, result );
    }

    /// <summary>
    /// Редактировать игрока
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateGamerModel"></param>
    /// <returns></returns>
    [HttpPut( "{id}" )]
    [ProducesResponseType<IActionResult>( StatusCodes.Status404NotFound )]
    [ProducesResponseType<IActionResult>( StatusCodes.Status204NoContent )]
    public async Task<IActionResult> EditGamerAsync( int id, UpdateGamerModel updateGamerModel )
    {
        var wasUpdated = await _service.UpdateAsync( id, _mapper.Map<UpdateGamerModel, UpdateGamerDto>( updateGamerModel ), Request.HttpContext.RequestAborted );

        if ( !wasUpdated )
        {
            return NotFound( $"Игрок с id = {id} не найден" );
        }

        return NoContent();
    }

    /// <summary>
    /// Удалить игрока по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType<IActionResult>( StatusCodes.Status404NotFound )]
    [ProducesResponseType<IActionResult>( StatusCodes.Status200OK )]
    public async Task<IActionResult> DeleteGamerAsync( int id )
    {
        var wasDeleted = await _service.DeleteAsync( id, Request.HttpContext.RequestAborted );

        if ( !wasDeleted )
        {
            return NotFound( $"Игрок с id = {id} не найден" );
        }

        return Ok();
    }
}
