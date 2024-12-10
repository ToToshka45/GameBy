using RatingService.Domain.Enums;

namespace RatingService.Application.Models.Dtos;

public record CreateEventDto(string Title, int ExternalEventId, DateTime CreationDate, Category Category, EventProgressionState State);
