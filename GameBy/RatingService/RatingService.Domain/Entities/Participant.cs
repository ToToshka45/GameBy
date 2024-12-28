using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public class Participant : Entity<int>
{
    public int EventId { get; private set; }
    /// <summary>
    /// The Id of a Participant by which its Entity is stored in the Event Service.
    /// </summary>
    //public int ExternalParticipantId { get; }
    
    /// <summary>
    /// The Id of a User by which its entity is stored in the Event Service.
    /// </summary>
    public int UserId { get; }
    public ParticipationState ParticipationState { get; private set; }
    public Rating Rating { get; }

    public Participant(
        int externalParticipantId,
        int externalUserId,
        //int externalEventId,
        ParticipationState participationState)
    {
        Id = externalParticipantId;
        UserId = externalUserId;
        //ExternalEventId = externalEventId;
        //ExternalParticipantId = externalParticipantId;
        ParticipationState = participationState;

        Rating = new Rating(Id, EntityType.Participant);
    }

    private Participant() { }

    public void SetState(ParticipationState state)
    {
        ParticipationState = state;
    }

    public void SetInnerEventRelation(int eventId) => EventId = eventId;
}
