using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public class Participant : Entity<int>
{
    public int ExternalParticipantId { get; }
    public int ExternalEventId { get; }
    public int ExternalUserId { get; }
    public ParticipationState ParticipationState { get; private set; }
    public UserRating Rating { get; }

    public Participant(
        int participantId,
        int userId,
        int eventId,
        UserRating rating,
        ParticipationState participationState)
    {
        ExternalUserId = userId;
        Rating = rating;
        ExternalEventId = eventId;
        ExternalParticipantId = participantId;
        ParticipationState = participationState;
    }

    private Participant() { }
    public void SetState(ParticipationState state)
    {
        ParticipationState = state;
    }
}
