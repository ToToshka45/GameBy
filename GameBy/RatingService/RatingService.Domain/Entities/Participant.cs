using RatingService.Domain.Entities.Ratings;
using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public class Participant : Entity<int>
{
    public int ExternalEventId { get; private set; }
    /// <summary>
    /// The Id of a Participant by which its Entity is stored in the Event Service.
    /// </summary>
    public int ExternalParticipantId { get; }
    /// <summary>
    /// The Id of a User by which its entity is stored in the Event Service.
    /// </summary>
    public int ExternalUserId { get; }
    public ParticipationState ParticipationState { get; private set; }
    public ParticipantRating Rating { get; }

    public Participant(
        int externalParticipantId,
        int externalUserId,
        int externalEventId,
        ParticipationState participationState)
    {
        ExternalParticipantId = externalParticipantId;
        ExternalUserId = externalUserId;
        ExternalEventId = externalEventId;
        ParticipationState = participationState;

        Rating = new(externalParticipantId);
    }

    private Participant() { }

    public void SetState(ParticipationState state)
    {
        ParticipationState = state;
    }
}
