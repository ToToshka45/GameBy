using RatingService.Domain.Entities;
using RatingService.Domain.Enums;

namespace RatingService.Application.Models.Dtos.Events;

public record GetEventDto(int Id, string Title, int ExternalEventId, DateTime CreationDate, EventCategory Category, EventProgressionState State, Rating Rating);
