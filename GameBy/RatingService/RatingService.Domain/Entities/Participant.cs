using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public class Participant : Entity<int>
{
    public int EventId { get; private set; }
    /// <summary>
    /// The Id of a Participant by which its Entity is stored in the Event Service.
    /// </summary>
    public int ExternalParticipantId { get; }
    /// <summary>
    /// The Id of an Event by which its Entity is stored in the Event Service.
    /// </summary>
    public int ExternalEventId { get; }
    /// <summary>
    /// The Id of a User by which its entity is stored in the Event Service.
    /// </summary>
    public int ExternalUserId { get; }
    public ParticipationState ParticipationState { get; private set; }
    public ParticipantRating Rating { get; }

    public Participant(
        int participantId,
        int userId,
        int externalEventId,
        ParticipationState participationState)
    {
        ExternalUserId = userId;
        ExternalEventId = externalEventId;
        ExternalParticipantId = participantId;
        ParticipationState = participationState;

        Rating = new ParticipantRating(Id);
    }

    private Participant() { }

    public void SetState(ParticipationState state)
    {
        ParticipationState = state;
    }

    public void SetInnerEventRelation(int eventId) => EventId = eventId;
}
