namespace RatingService.Domain.Entities.Ratings;

public class ParticipantRating : IntermediateRating
{
    public int ExternalParticipantId { get; }
    public ParticipantRating(int externalParticipantId) 
    {
        ExternalParticipantId = externalParticipantId;
    }

    private ParticipantRating() { }
}

