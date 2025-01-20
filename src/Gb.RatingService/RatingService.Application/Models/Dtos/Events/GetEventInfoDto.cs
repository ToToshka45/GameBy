using RatingService.Common.Enums;

namespace RatingService.Application.Models.Dtos.Events;

public record GetEventInfoDto(int Id,
                          string Title,
                          int OrganizerId,
                          DateTime CreationDate,
                          EventCategory Category,
                          EventProgressionState State,
                          float? Rating);
