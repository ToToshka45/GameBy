namespace RatingService.Domain.Entities;

public class ParticipantRatingUpdate : RatingUpdate
{
    public int RatingId { get; private set; }
    public ParticipantRatingUpdate(float value, int authorId, int receipientId, int eventId, DateTime creationDate)
        : base(value, authorId, receipientId, creationDate)
    {
        SubjectId = receipientId;
    }

    private ParticipantRatingUpdate() { }
    public void SetRatingRelation(int ratingId) => RatingId = ratingId;
}
