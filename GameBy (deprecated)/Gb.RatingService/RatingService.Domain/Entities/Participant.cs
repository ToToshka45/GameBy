using RatingService.Domain.Entities.Ratings;
using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Entities;

public class Participant : Entity<int>
{
    //public int ExternalParticipantId { get; }
    //public int ExternalEventId { get; private set; }
    //public int ExternalUserId { get; }

    public ParticipationState ParticipationState { get; private set; }
    public ParticipantRating? Rating { get; private set; }

    // relations keys
    public int UserInfoId { get; private set; }
    public int EventInfoId { get; private set; }

    public Participant(
        int externalParticipantId,
        int externalUserId,
        int externalEventId,
        ParticipationState participationState)
    {
        Id = externalParticipantId;
        UserInfoId = externalUserId;
        EventInfoId = externalEventId;
        //ExternalParticipantId = externalParticipantId;
        //ExternalUserId = externalUserId;
        //ExternalEventId = externalEventId;
        ParticipationState = participationState;

        SetInitialRating(externalParticipantId, externalUserId);
    }

    private Participant() { }
    public void SetInitialRating(int externalParticipantId, int externalUserId)
    {
        if (Rating is null) Rating = new(externalParticipantId, externalUserId);
    }

    public void SetState(ParticipationState state)
    {
        ParticipationState = state;
    }
}
