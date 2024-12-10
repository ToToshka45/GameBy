using RatingService.Application.Models.Dtos;
using RatingService.Domain.Aggregates;

namespace RatingService.Application.Configurations.Mappings;

internal static class MappingExtensions
{
    public static EventInfo ToEventInfo(this CreateEventDto dto) =>
        new(dto.Title, dto.ExternalEventId, dto.CreationDate, dto.Category, dto.State);
}
