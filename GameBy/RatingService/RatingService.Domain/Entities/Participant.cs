using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities;

public class Participant : Entity<int>
{
    public int ExternalParticipantId { get; }
    public int ExternalEventId { get; }
    public int ExternalUserId { get; }
    public ParticipationState ParticipationState { get; private set; }
    public ParticipantRating Rating { get; }

    public Participant(
        int participantId,
        int userId,
        int eventId,
        ParticipationState participationState, 
        Category category)
    {
        ExternalUserId = userId;
        ExternalEventId = eventId;
        ExternalParticipantId = participantId;
        ParticipationState = participationState;

        Rating = new ParticipantRating(Id, category);
    }

    private Participant() { }
    public void SetState(ParticipationState state)
    {
        ParticipationState = state;
    }
}
