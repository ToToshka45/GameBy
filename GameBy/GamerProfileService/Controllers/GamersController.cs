using GameBy.Core.Abstractions.Repositories;
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
        var response = gamers.Select( x =>
            new GamerResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Nickname = x.Nickname,
                //DateOfBirth = x.DateOfBirth,
                AboutMe = x.AboutMe,
                Country = x.Country,
                City = x.City,
                ContactMe = x.ContactMe,
            } ).ToList();

        return Ok( response );
    }

    ///// <summary>
    ///// Получить данные игрока по идентификатору
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //[HttpGet( "{id}" )]
    //public async Task<ActionResult<CustomerResponse>> GetCustomerAsync( Guid id )
    //{
    //    var entityItem = await _unitOfWork.CustomerRepository.GetAsync( id, Request.HttpContext.RequestAborted );

    //    var preferenceResponses = new List<PreferenceResponse>();
    //    var preferences = await _unitOfWork.PreferenceRepository.GetAsyncByIds( entityItem.CustomerPreferences.Select( cp => cp.PreferenceId ), Request.HttpContext.RequestAborted );
    //    foreach ( var preference in preferences )
    //    {
    //        var preferenceResponse = new PreferenceResponse()
    //        {
    //            Id = preference.Id,
    //            Name = preference.Name,
    //        };

    //        preferenceResponses.Add( preferenceResponse );
    //    }

    //    var response = new CustomerResponse()
    //    {
    //        Id = entityItem.Id,
    //        FirstName = entityItem.FirstName,
    //        LastName = entityItem.LastName,
    //        Email = entityItem.Email,
    //        PromoCodes = entityItem.PromoCodes.Select( pc => {
    //            return new PromoCodeResponse()
    //            {
    //                Id = pc.Id,
    //                Code = pc.Code,
    //                ServiceInfo = pc.ServiceInfo,
    //                BeginDate = pc.BeginDate.ToString(),
    //                EndDate = pc.EndDate.ToString(),
    //                PartnerName = pc.PartnerName,
    //            };
    //        } ).ToList(),
    //        Preferences = preferenceResponses,
    //    };

    //    return Ok( response );
    //}

    /// <summary>
    /// Создать нового игрока
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateCustomerAsync( CreateOrEditGamerRequest request )
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

        var createdCustomer = await _unitOfWork.GamerRepository.AddAsync( newGamer, Request.HttpContext.RequestAborted );

        await _unitOfWork.SaveChangesAsync( Request.HttpContext.RequestAborted );

        return Created();
    }

    ///// <summary>
    ///// Редактировать игрока
    ///// </summary>
    ///// <param name="id"></param>
    ///// <param name="request"></param>
    ///// <returns></returns>
    //[HttpPut( "{id}" )]
    //public async Task<IActionResult> EditCustomersAsync( Guid id, CreateOrEditCustomerRequest request )
    //{
    //    var entityItem = await _unitOfWork.CustomerRepository.GetAsync( id, Request.HttpContext.RequestAborted );

    //    var customerPreferences = new List<CustomerPreference>();
    //    var preferences = await _unitOfWork.PreferenceRepository.GetAsyncByIds( request.PreferenceIds, Request.HttpContext.RequestAborted );
    //    foreach ( var preference in preferences )
    //    {
    //        var customerPreference = new CustomerPreference()
    //        {
    //            PreferenceId = preference.Id,
    //            Preference = preference,
    //        };

    //        customerPreferences.Add( customerPreference );
    //    }

    //    entityItem.FirstName = request.FirstName;
    //    entityItem.LastName = request.LastName;
    //    entityItem.Email = request.Email;
    //    entityItem.CustomerPreferences = customerPreferences;

    //    await _unitOfWork.CustomerRepository.UpdateAsync( entityItem, Request.HttpContext.RequestAborted );
    //    await _unitOfWork.SaveChangesAsync( Request.HttpContext.RequestAborted );

    //    return NoContent();
    //}

    ///// <summary>
    ///// Удалить игрока по идентификатору
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //[HttpDelete]
    //public async Task<IActionResult> DeleteCustomer( Guid id )
    //{
    //    var wasDeleted = await _unitOfWork.CustomerRepository.DeleteAsync( id, Request.HttpContext.RequestAborted );

    //    if ( wasDeleted )
    //    {
    //        await _unitOfWork.SaveChangesAsync( Request.HttpContext.RequestAborted );
    //    }

    //    return Ok( wasDeleted );
    //}
}
