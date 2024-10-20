﻿using GameBy.Core.Abstractions.Repositories;
using GameBy.Core.Domain.Entities;
using GamerProfileService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamerProfileService.Controllers;

/// <summary>
/// Игроки
/// </summary>
[ApiController]
[Route( "api/v1/[controller]" )]
public class GamersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public GamersController( IUnitOfWork unitOfWork )
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Получить данные всех игроков
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<GamerResponse>>> GetGamersAsync()
    {
        var gamers = await _unitOfWork.GamerRepository.GetAllAsync( Request.HttpContext.RequestAborted );
        var response = gamers.Select( gamer =>
            new GamerResponse()
            {
                Id = gamer.Id,
                Name = gamer.Name,
                Nickname = gamer.Nickname,
                //DateOfBirth = gamer.DateOfBirth,
                AboutMe = gamer.AboutMe,
                Country = gamer.Country,
                City = gamer.City,
                ContactMe = gamer.ContactMe,
            } ).ToList();

        return Ok( response );
    }

    /// <summary>
    /// Получить данные игрока по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet( "{id}" )]
    public async Task<ActionResult<GamerResponse>> GetGamerAsync( int id )
    {
        var gamer = await _unitOfWork.GamerRepository.GetAsync( id, Request.HttpContext.RequestAborted );

        if ( gamer is null )
        {
            return NotFound();
        }

        var response = new GamerResponse()
        {
            Id = gamer.Id,
            Name = gamer.Name,
            Nickname = gamer.Nickname,
            //DateOfBirth = gamer.DateOfBirth,
            AboutMe = gamer.AboutMe,
            Country = gamer.Country,
            City = gamer.City,
            ContactMe = gamer.ContactMe,
        };

        return Ok( response );
    }

    /// <summary>
    /// Создать нового игрока
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateGamerAsync( CreateOrEditGamerRequest request )
    {
        var newGamer = new Gamer()
        {
            Name = request.Name,
            Nickname = request.Nickname,
            //DateOfBirth = request.DateOfBirth,
            AboutMe = request.AboutMe,
            Country = request.Country,
            City = request.City,
            ContactMe = request.ContactMe,
        };

        await _unitOfWork.GamerRepository.AddAsync( newGamer, Request.HttpContext.RequestAborted );

        await _unitOfWork.SaveChangesAsync( Request.HttpContext.RequestAborted );

        return Created();
    }

    /// <summary>
    /// Редактировать игрока
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut( "{id}" )]
    public async Task<IActionResult> EditGamerAsync( int id, CreateOrEditGamerRequest request )
    {
        var gamer = await _unitOfWork.GamerRepository.GetAsync( id, Request.HttpContext.RequestAborted );

        if ( gamer is null )
        {
            return NotFound();
        }

        gamer.Name = request.Name;
        gamer.Nickname = request.Nickname;
        //gamer.DateOfBirth = request.DateOfBirth;
        gamer.AboutMe = request.AboutMe;
        gamer.Country = request.Country;
        gamer.City = request.City;
        gamer.ContactMe = request.ContactMe;

        await _unitOfWork.GamerRepository.UpdateAsync( gamer, Request.HttpContext.RequestAborted );
        await _unitOfWork.SaveChangesAsync( Request.HttpContext.RequestAborted );

        return NoContent();
    }

    /// <summary>
    /// Удалить игрока по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteGamerAsync( int id )
    {
        var wasDeleted = await _unitOfWork.GamerRepository.DeleteAsync( id, Request.HttpContext.RequestAborted );

        if ( wasDeleted )
        {
            await _unitOfWork.SaveChangesAsync( Request.HttpContext.RequestAborted );
        }

        return Ok( wasDeleted );
    }
}
