using AutoMapper;
using Domain.Entities;
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
    private readonly IAchievementService _achievementService;
    private readonly IMapper _mapper;

    public GamerController( ILogger<GamerController> logger, IGamerService gamerService, IRankService rankService, IAchievementService achievementService, IMapper mapper )
    {
        _logger = logger;

        _gamerService = gamerService;
        _rankService = rankService;
        _achievementService = achievementService;
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

    /// <summary>
    /// Установить звание игроку
    /// </summary>
    /// <param name="id"></param>
    /// <param name="setGamerRankModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut( "set-rank/{id}" )]
    [ProducesResponseType<string>( StatusCodes.Status404NotFound )]
    [ProducesResponseType( StatusCodes.Status204NoContent )]
    public async Task<IActionResult> SetGamerRankAsync( int id, SetGamerRankModel setGamerRankModel, CancellationToken cancellationToken )
    {
        var rankId = setGamerRankModel.RankId;
        var rankDto = await _rankService.GetByIdAsync( rankId, cancellationToken );

        if ( rankDto is null )
        {
            return NotFound( $"Звание с id = {rankId} не найдено" );
        }

        var wasUpdated = await _gamerService.SetRankAsync( id, _mapper.Map<SetGamerRankModel, SetGamerRankDto>( setGamerRankModel ), cancellationToken );

        return wasUpdated ? NoContent() : NotFound( $"Игрок с id = {id} не найден" );
    }

    /// <summary>
    /// Дать игроку достижение
    /// </summary>
    /// <param name="id"></param>
    /// <param name="giveAchievementToGamerModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut( "give-achievement/{id}" )]
    [ProducesResponseType<string>( StatusCodes.Status404NotFound )]
    [ProducesResponseType( StatusCodes.Status204NoContent )]
    public async Task<IActionResult> GiveAchievementToGamerAsync( int id, GiveAchievementToGamerModel giveAchievementToGamerModel, CancellationToken cancellationToken )
    {
        var achievementId = giveAchievementToGamerModel.AchievementId;
        var achievementDto = await _achievementService.GetByIdAsync( achievementId, cancellationToken );

        if ( achievementDto is null )
        {
            return NotFound( $"Достижение с id = {achievementDto} не найдено" );
        }

        var wasUpdated = await _gamerService.GiveAchievementAsync( id, _mapper.Map<GiveAchievementToGamerModel, GiveAchievementToGamerDto>( giveAchievementToGamerModel ), cancellationToken );

        return wasUpdated ? NoContent() : NotFound( $"Игрок с id = {id} не найден" );
    }

    /// <summary>
    /// Получить данные о достижениях игрока по его идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet( "earned-achievements/{id}" )]
    [ProducesResponseType<string>( StatusCodes.Status404NotFound )]
    [ProducesResponseType<List<GamerModel>>( StatusCodes.Status200OK )]
    public async Task<ActionResult<List<AchievementModel>>> GetEarnedAchievementsAsync( int id, CancellationToken cancellationToken )
    {
        var gamerDto = await _gamerService.GetByIdAsync( id, cancellationToken );
        if ( gamerDto is null )
        {
            return NotFound( $"Игрок с id = {id} не найден" );
        }

        var earnedAchievementsDtos = await _gamerService.GetEarnedAchievementsByIdAsync( id, cancellationToken );

        return _mapper.Map<List<AchievementDto>, List<AchievementModel>>( earnedAchievementsDtos );
    }

    /// <summary>
    /// Получить данные о доступных званиях игрока по его идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet( "available-ranks/{id}" )]
    [ProducesResponseType<string>( StatusCodes.Status404NotFound )]
    [ProducesResponseType<List<GamerModel>>( StatusCodes.Status200OK )]
    public async Task<ActionResult<List<RankModel>>> GetAvailableRanksAsync( int id, CancellationToken cancellationToken )
    {
        var gamerDto = await _gamerService.GetByIdAsync( id, cancellationToken );
        if ( gamerDto is null )
        {
            return NotFound( $"Игрок с id = {id} не найден" );
        }

        var availableRanksDtos = await _gamerService.GetAvailableRanksByIdAsync( id, cancellationToken );

        return _mapper.Map<List<RankDto>, List<RankModel>>( availableRanksDtos );
    }

}
