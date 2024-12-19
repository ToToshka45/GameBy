﻿using Microsoft.Extensions.Logging;
using RatingService.Application.Abstractions;
using RatingService.Application.Configurations.Mappings;
using RatingService.Application.Models.Dtos.Events;
using RatingService.Domain.Abstractions;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;

namespace RatingService.Application.Services;

public class EventLifecycleService : IEventLifecycleService
{
    private readonly IRepository<EventInfo> _eventRepo;
    private readonly IRepository<UserInfo> _userRepo;
    private readonly ILogger<EventLifecycleService> _logger;

    public EventLifecycleService(IRepository<EventInfo> eventRepo, ILogger<EventLifecycleService> logger, IRepository<UserInfo> userRepo)
    {
        _eventRepo = eventRepo;
        _logger = logger;
        _userRepo = userRepo;
    }

    public async Task<int?> AddNewEventAsync(CreateEventDto newEvent, CancellationToken token)
    {
        try
        {
            var eventInfo = newEvent.ToEventInfo();
            var savedEvent = await _eventRepo.Add(eventInfo, token);
            return savedEvent.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while adding a new Event instance.");
            throw;
        }
    }

    public async Task<GetEventDto?> GetEventByIdAsync(int id, CancellationToken token)
    {
        try
        {
            var eventInfo = await _eventRepo.GetById(id, token);
            if (eventInfo is null) return null;
            return eventInfo.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while retrieveing a stored Event instance.");
            throw;
        }
    }

    public async Task<ICollection<GetEventDto>> GetEventsAsync(CancellationToken token)
    {
        var events = await _eventRepo.GetAll(token);
        return events.ToGetEventsDto();
    }

    public async Task AddParticipantAsync(int eventId, Participant participant, CancellationToken token)
    {
        try
        {
            var storedEvent = await _eventRepo.GetById(eventId, token);
            if (storedEvent is null) return;

            storedEvent.AddParticipant(participant);
            await _eventRepo.Update(eventId, storedEvent, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while trying to add a new Participant to an Event.");
            throw;
        }
    }
}
