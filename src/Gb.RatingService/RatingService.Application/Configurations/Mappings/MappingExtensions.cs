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
        new(dto.ExternalEventId, dto.Title, dto.OrganizerId, dto.CreationDate.ToUniversalTime(), dto.Category, dto.State);
    public static EventInfo ToEventInfo(this FinalizeEventDto dto) =>
        new(dto.EventId, dto.Title, dto.OrganizerId, dto.CreationDate.ToUniversalTime(), dto.Category, dto.State);
    public static GetEventInfoDto ToGetEventInfoDto(this EventInfo @event) =>
        new(@event.Id, @event.Title, @event.OrganizerId, @event.CreationDate, @event.Category, @event.State, @event.Rating?.Value);
    public static ICollection<GetEventInfoDto> ToDtoList(this IEnumerable<EventInfo> events) =>
        events.Select(e => e.ToGetEventInfoDto()).ToList();

    // users
    public static UserInfo ToUserInfo(this AddUserDto dto) =>
        new(dto.ExternalUserId, dto.UserName);
    public static IEnumerable<GetUserInfoDto> ToGetUserInfoDtoList(this IEnumerable<UserInfo> data) =>
         data.Select(ui => ui.ToGetUserInfoDto()).ToList();
    public static GetUserInfoDto ToGetUserInfoDto(this UserInfo req) =>
         new(req.Id, req.UserName, req.GamerRating?.Value, req.OrganizerRating?.Value);
    public static GetUserRatingsDto ToGetUserRatingsDto(this UserInfo user) =>
        new(user.Id, user.GamerRating, user.OrganizerRating);
    public static GetUserFeedbacksDto ToGetUserFeedbacksDto(this UserInfo user) =>
        new(user.Id, user.GamerFeedbacks, user.OrganizerFeedbacks);

    // Participants

    internal static Participant ToParticipant(this AddParticipantDto dto) =>
        new(dto.ExternalParticipantId, dto.ExternalUserId, dto.ExternalEventId, dto.State);
    internal static IEnumerable<Participant> ToParticipantsList(this IEnumerable<AddParticipantDto> dtoList) =>
        dtoList.Select(p => p.ToParticipant()).ToList();
    internal static GetParticipantDto ToDto(this Participant participant) =>
        new(participant.Id, participant.UserInfoId, participant.EventInfoId, participant.ParticipationState, participant.Rating?.Value);
    internal static IEnumerable<GetParticipantDto> ToDtoList(this IEnumerable<Participant> participants) =>
        participants.Select(e => e.ToDto()).ToList();

    // Ratings

    internal static ParticipantRatingUpdate ToRatingUpdate(this AddParticipantRatingUpdateDto dto) =>
        new(dto.Value, dto.AuthorId, dto.ReceipientId, dto.EventId, dto.CreationDate.ToUniversalTime());
    internal static EventRatingUpdate ToRatingUpdate(this AddEventRatingUpdateDto dto) =>
        new(dto.Value, dto.AuthorId, dto.EventId, dto.CreationDate.ToUniversalTime());

    //public static EventRatingUpdate ToRatingUpdate(this AddEventRatingUpdateDto dto) =>
    //    new(dto.SubjectId, dto.Value, dto.AuthorId, dto.EventId, dto.CreationDate.ToUniversalTime());

}
