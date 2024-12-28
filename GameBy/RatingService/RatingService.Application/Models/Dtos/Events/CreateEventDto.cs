using RatingService.Domain.Enums;

namespace RatingService.Application.Models.Dtos.Events;

public record CreateEventDto(string Title,
                             int ExternalEventId,
                             DateTime CreationDate,
                             EventCategory Category,
                             EventProgressionState State);
