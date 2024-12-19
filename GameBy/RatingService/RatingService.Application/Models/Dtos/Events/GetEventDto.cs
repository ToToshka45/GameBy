using RatingService.Domain.Entities;
using RatingService.Domain.Enums;

namespace RatingService.Application.Models.Dtos.Events;

public record GetEventDto(string Title, int ExternalEventId, DateTime CreationDate, Category Category, EventProgressionState State, Rating Rating);
