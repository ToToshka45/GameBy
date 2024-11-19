using RatingService.Domain.Aggregates;
using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Entities;

public class Participant : Entity<int>
{
    public ExternalParticipantId ParticipantId { get; }
    public ExternalEventId EventId { get; }
    public ExternalUserId UserId { get; }
    public ParticipationState ParticipationState { get; private set; }
    public Rating Rating { get; }

    public Participant(
        int id,
        ExternalParticipantId participantId,
        ExternalUserId userId,
        ExternalEventId eventId,
        Rating rating,
        ParticipationState participationState) : base(id)
    {
        UserId = userId;
        Rating = rating;
        EventId = eventId;
        ParticipantId = participantId;
        ParticipationState = participationState;
    }

    public void SetState(ParticipationState state)
    {
        ParticipationState = state;
    }
}
