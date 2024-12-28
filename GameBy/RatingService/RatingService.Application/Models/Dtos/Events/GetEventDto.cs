using RatingService.Domain.Entities;
using RatingService.Domain.Enums;

namespace RatingService.Application.Models.Dtos.Events;

public record GetEventDto(int ExternalEventId,
                          string Title,
                          DateTime CreationDate,
                          EventCategory Category,
                          EventProgressionState State,
                          Rating Rating);
