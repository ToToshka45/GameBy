using RatingService.Domain.Aggregates;
using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Entities;

public class ParticipantInfo : Entity<int>
{
    public ParticipantId ParticipantId { get; }
    public UserId UserId { get; }
    public EventId EventId { get; }
    public ParticipationState ParticipationState { get; }
    public ParticipantRating Rating { get; }

    public ParticipantInfo(
        int id,
        ParticipantId participantId,
        UserId userId,
        EventId eventId,
        ParticipantRating rating,
        ParticipationState participationState) : base(id)
    {
        UserId = userId;
        Rating = rating;
        EventId = eventId;
        ParticipantId = participantId;
        ParticipationState = participationState;
    }
}
