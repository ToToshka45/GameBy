using RatingService.API.Models;
using RatingService.Application.Models.Dtos;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Enums;

namespace RatingService.API.Configurations.Mappings;

internal static class MappingExtensions
{
    internal static CreateEventDto ToDto(this CreateEventRequest req) =>
        new(req.Title, req.EventId, req.CreationDate, Enum.Parse<Category>(req.Category)); // TODO: check that a passed category exists, somewhere before the mapping

    internal static IEnumerable<GetEventInfoResponse> ToShortResponseList(this IEnumerable<EventInfo> events)
    {
        var responseList = new List<GetEventInfoResponse>();
        foreach (var e in events)
        {
            responseList.Add(new()
            {
                Id = e.Id,
                Title = e.Title,
                Category = e.Category.ToString(),
                CreatedAt = e.CreatedAt,
                ExternalEventId = e.ExternalEventId,
                Rating = e.Rating.Value
            });
        }
        return responseList;
    }
}
