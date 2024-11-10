using RatingService.Domain.Enums;
using RatingService.Domain.Models.Primitives;
using RatingService.Domain.Models.ValueObjects;
using RatingService.Domain.Models.ValueObjects.Identifiers;

namespace RatingService.Domain.Models.Entities;

public class ParticipantInfo : Entity
{
    public ParticipantId ParticipantId { get; }
    public UserId UserId { get; }
    public EventId EventId { get; }
    public ParticipationState ParticipationState { get; }
    public Rating Rating { get; }
    private ParticipantInfo(ParticipantId participantId, UserId userId, EventId eventId, Rating rating, 
        ParticipationState participationState)
    {
        UserId = userId;
        Rating = rating;
        EventId = eventId;
        ParticipantId = participantId;
        ParticipationState = participationState;
    }

    public static ParticipantInfo Create(ParticipantId participantId, UserId userId, EventId eventId, Rating rating,
        ParticipationState participationState) => new ParticipantInfo(participantId, userId, eventId, rating, participationState);
}
