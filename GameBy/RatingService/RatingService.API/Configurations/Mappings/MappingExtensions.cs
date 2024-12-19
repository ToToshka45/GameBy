using RatingService.API.Models.Events;
using RatingService.API.Models.Users;
using RatingService.Application.Models.Dtos.Events;
using RatingService.Application.Models.Dtos.Users;
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

    internal static AddUserDto ToDto(this AddUserRequest req) =>
        new(req.Id, req.UserName);

    internal static IEnumerable<GetEventResponse> ToResponseList(this IEnumerable<GetEventDto> events)
    {
        var responseList = new List<GetEventResponse>();
        foreach (var e in events)
        {
            responseList.Add(new()
            {
                Title = e.Title,
                Category = e.Category.ToString(),
                CreationDate = e.CreationDate,
                ExternalEventId = e.ExternalEventId,
                Rating = e.Rating.Value
            });
        }
        return responseList;
    }

    //internal static IEnumerable<GetUserRatingsResponse> ToResponseList(this IEnumerable<GetUserRatingsDto> events)
    //{
    //    var responseList = new List<GetUserRatingsResponse>();
    //    foreach (var e in events)
    //    {
    //        responseList.Add(new(e.ExternalUserId, e.Ratings));
    //    }
    //    return responseList;
    //}

    internal static CreateEventResponse ToResponse(this EventInfo eventCreated) => new() { 
        Id = eventCreated.Id,
        Title = eventCreated.Title,
        CreationDate = eventCreated.CreationDate,
        Category = eventCreated.Category.ToString(),
        State = eventCreated.State.ToString(),
        EventId = eventCreated.ExternalEventId
    };

    internal static GetUserResponse ToResponse(this GetUserDto dto) => new(dto.ExternalUserId, dto.UserName);

    // Ratings and Feedbacks mappings
    internal static GetUserRatingsResponse ToResponse(this GetUserRatingsDto req) =>
        new(req.ExternalUserId, req.Ratings);
    internal static GetUserFeedbacksResponse ToResponse(this GetUserFeedbacksDto req) =>
        new(req.ExternalUserId, req.GamerRatings, req.OrganizerRatings);
}
