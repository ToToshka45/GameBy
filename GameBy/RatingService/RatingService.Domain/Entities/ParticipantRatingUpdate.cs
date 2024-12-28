namespace RatingService.Domain.Entities;

public class ParticipantRatingUpdate : RatingUpdate
{
    public ParticipantRatingUpdate(int receipientId, float value, int authorId, int eventId, DateTime creationDate)
        : base(value, authorId, receipientId, eventId, creationDate)
    {
        SubjectId = receipientId;
    }
    private ParticipantRatingUpdate() { }
}
