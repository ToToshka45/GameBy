﻿using RatingService.API.Models;
using RatingService.API.Models.Events;
using RatingService.API.Models.Participants;
using RatingService.API.Models.Ratings;
using RatingService.API.Models.Users;
using RatingService.Application.Models.Dtos.Events;
using RatingService.Application.Models.Dtos.Participants;
using RatingService.Application.Models.Dtos.Ratings;
using RatingService.Application.Models.Dtos.Users;
using RatingService.Common.Models.Extensions;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Enums;

namespace RatingService.API.Configurations.Mappings;

internal static class MappingExtensions
{
    // Events

    internal static CreateEventDto ToDto(this CreateEventRequest req) => 
        new(req.Title, req.EventId, req.CreationDate, 
            req.Category.TryParseOrDefault(EventCategory.Unclarified),
            req.State.TryParseOrDefault(EventProgressionState.Announced));

    internal static GetEventResponse ToResponse(this GetEventDto req) =>
        new(req.ExternalEventId, req.Title, req.CreationDate, req.Category.ToString(), req.State.ToString(), req.Rating.Value);

    internal static IEnumerable<GetEventResponse> ToResponseList(this IEnumerable<GetEventDto> events)
    {
        var responseList = new List<GetEventResponse>();
        foreach (var e in events)
        {
            responseList.Add(e.ToResponse());
        }
        return responseList;
    }

    internal static CreateEventResponse ToResponse(this EventInfo eventCreated) => new() { 
        Id = eventCreated.Id,
        Title = eventCreated.Title,
        CreationDate = eventCreated.CreationDate,
        Category = eventCreated.Category.ToString(),
        State = eventCreated.State.ToString(),
        //EventId = eventCreated.ExternalEventId
    };

    internal static FinalizeEventDto ToDto(this FinalizeEventRequest req) =>
        new(req.ExternalEventId, req.State.TryParseOrDefault(EventProgressionState.Unclarified), req.Participants.Select(p => p.ToDto()).ToList());

    // Users

    internal static AddUserDto ToDto(this AddUserRequest req) =>
        new(req.ExternalUserId, req.UserName);
    internal static GetUserResponse ToResponse(this GetUserDto dto) => 
        new(dto.ExternalUserId, dto.UserName);

    // Participants

    internal static AddParticipantDto ToDto(this AddParticipantRequest req) =>
        new(req.ExternalParticipantId, req.ExternalUserId, req.State.TryParseOrDefault(ParticipationState.Unclarified));
    internal static ParticipantStateChangeDto ToDto(this ParticipantStateChangeRequest req) =>
        new(req.ExternalParticipantId, req.State.TryParseOrDefault(ParticipationState.Unclarified));

    // Ratings and Feedbacks

    internal static AddParticipantRatingUpdateDto ToDto(this AddParticipantRatingUpdateRequest req) =>
        new(req.Value, req.EntityType.TryParseOrDefault(EntityType.Participant), req.AuthorId, req.SubjectId, req.EventId, req.CreationDate);
    //internal static EditParticipantRatingDto ToDto(this EditParticipantRatingRequest req) => new(req.Value);
    internal static GetUserRatingsResponse ToResponse(this GetUserRatingsDto req) =>
        new(req.ExternalUserId, req.GamerRating, req.OrganizerRating);
    internal static GetUserFeedbacksResponse ToResponse(this GetUserFeedbacksDto req) =>
        new(req.ExternalUserId, req.GamerFeedbacks, req.OrganizerFeedbacks);
}
