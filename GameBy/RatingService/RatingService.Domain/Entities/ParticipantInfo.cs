using RatingService.Domain.Aggregates;
using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Entities;

public class ParticipantInfo : Entity<int>
{
    public ParticipantId ParticipantId { get; }
    public UserId UserId { get; }
    public Event Event { get; }
    public ParticipationState ParticipationState { get; private set; }
    public ParticipantRating Rating { get; }

    public ParticipantInfo(
        int id,
        ParticipantId participantId,
        UserId userId,
        Event eventEntity,
        ParticipantRating rating,
        ParticipationState participationState) : base(id)
    {
        UserId = userId;
        Rating = rating;
        Event = eventEntity;
        ParticipantId = participantId;
        ParticipationState = participationState;
    }

    public void SetState(ParticipationState state)
    {
        ParticipationState = state;
    }
}
