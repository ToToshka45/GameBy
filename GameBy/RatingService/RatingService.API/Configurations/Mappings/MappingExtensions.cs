using RatingService.API.Models;
using RatingService.Application.Models.Dtos;
using RatingService.Common.Models.Extensions;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Enums;

namespace RatingService.API.Configurations.Mappings;

internal static class MappingExtensions
{
    internal static CreateEventDto ToDto(this CreateEventRequest req) => 
        new(req.Title, req.EventId, req.CreationDate, 
            req.Category.TryParseOrDefault(Category.Unknown),
            req.State.TryParseOrDefault(EventProgressionState.Announced));
    
    internal static IEnumerable<GetEventInfoResponse> ToResponseList(this IEnumerable<EventInfo> events)
    {
        var responseList = new List<GetEventInfoResponse>();
        foreach (var e in events)
        {
            responseList.Add(new()
            {
                Id = e.Id,
                Title = e.Title,
                Category = e.Category.ToString(),
                CreationDate = e.CreationDate,
                ExternalEventId = e.ExternalEventId,
                Rating = e.Rating.Value
            });
        }
        return responseList;
    }

    internal static CreateEventResponse ToResponse(this EventInfo eventCreated) => new() { 
        Id = eventCreated.Id,
        Title = eventCreated.Title,
        CreationDate = eventCreated.CreationDate,
        Category = eventCreated.Category.ToString(),
        State = eventCreated.State.ToString(),
        EventId = eventCreated.ExternalEventId
    };
}
