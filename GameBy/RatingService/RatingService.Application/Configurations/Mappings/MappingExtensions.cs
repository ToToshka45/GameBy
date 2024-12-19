using RatingService.Application.Models.Dtos.Events;
using RatingService.Application.Models.Dtos.Users;
using RatingService.Domain.Aggregates;

namespace RatingService.Application.Configurations.Mappings;

internal static class MappingExtensions
{
    // events
    public static EventInfo ToEventInfo(this CreateEventDto dto) =>
        new(dto.Title, dto.ExternalEventId, dto.CreationDate, dto.Category, dto.State);
    public static GetEventDto ToDto(this EventInfo @event) =>
        new(@event.Title, @event.ExternalEventId, @event.CreationDate, @event.Category, @event.State, @event.Rating);
    public static ICollection<GetEventDto> ToGetEventsDto(this IEnumerable<EventInfo> events) =>
        events.Select(e => new GetEventDto(e.Title, e.ExternalEventId, e.CreationDate, e.Category, e.State, e.Rating)).ToList();

    // user
    public static GetUserDto ToGetUserDto(this UserInfo dto) =>
        new(dto.ExternalUserId, dto.UserName);
    public static UserInfo ToUserInfo(this AddUserDto dto) =>
        new(dto.ExternalUserId, dto.UserName);
    public static GetUserRatingsDto ToGetUserRatingsDto(this UserInfo user) =>
        new(user.ExternalUserId, user.RatingsByCategory);
    public static GetUserFeedbacksDto ToGetUserFeedbacksDto(this UserInfo user) =>
        new(user.ExternalUserId, user.GamerFeedbacks, user.OrganizerFeedbacks);
        
}
