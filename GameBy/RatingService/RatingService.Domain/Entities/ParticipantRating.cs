namespace RatingService.Domain.Entities;

public class ParticipantRating : Rating
{
    public int ParticipantId { get; }
    public ParticipantRating(int participantId)
    {
        Value = 0;
        ParticipantId = participantId;
    }

    private ParticipantRating() { }
}

