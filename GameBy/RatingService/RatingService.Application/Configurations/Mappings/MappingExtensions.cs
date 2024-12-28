using RatingService.Application.Models.Dtos.Events;
using RatingService.Application.Models.Dtos.Participants;
using RatingService.Application.Models.Dtos.Ratings;
using RatingService.Application.Models.Dtos.Users;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;

namespace RatingService.Application.Configurations.Mappings;

internal static class MappingExtensions
{
    // events
    public static EventInfo ToEventInfo(this CreateEventDto dto) =>
        new(dto.Title, dto.ExternalEventId, dto.CreationDate.ToUniversalTime(), dto.Category, dto.State);
    public static GetEventDto ToDto(this EventInfo @event) =>
        new(@event.Id, @event.Title, @event.CreationDate, @event.Category, @event.State, @event.Rating);
    public static ICollection<GetEventDto> ToDtoList(this IEnumerable<EventInfo> events) =>
        events.Select(e => e.ToDto()).ToList();

    // users
    public static UserInfo ToUserInfo(this AddUserDto dto) =>
        new(dto.ExternalUserId, dto.UserName);
    public static GetUserDto ToDto(this UserInfo dto) =>
        new(dto.Id, dto.UserName);
    public static GetUserRatingsDto ToGetUserRatingsDto(this UserInfo user) =>
        new(user.Id, user.GamerRating, user.OrganizerRating);
    public static GetUserFeedbacksDto ToGetUserFeedbacksDto(this UserInfo user) =>
        new(user.Id, user.GamerFeedbacks, user.OrganizerFeedbacks);

    // Participants
    public static Participant ToParticipant(this AddParticipantDto dto) =>
        new(dto.ExternalParticipantId, dto.ExternalUserId, dto.State);

    // Ratings

    public static RatingUpdate ToRatingUpdate(this AddParticipantRatingUpdateDto dto) =>
        new(dto.Value, dto.AuthorId, dto.SubjectId, dto.EventId, dto.EntityType, dto.CreationDate);

}
