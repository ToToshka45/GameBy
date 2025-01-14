namespace RatingService.Domain.Entities.Ratings;

public class ParticipantRating : RatingBase
{
    public int ParticipantId { get; }
    public int GamerRatingId { get; }
    //public int ExternalParticipantId { get; }

    private List<ParticipantRatingUpdate> _updates = [];
    public IReadOnlyList<ParticipantRatingUpdate> Updates => _updates;

    public ParticipantRating(int externalParticipantId, int userInfoId)
    {
        Id = externalParticipantId;
        ParticipantId = externalParticipantId;
        GamerRatingId = userInfoId;
        //ExternalParticipantId = externalParticipantId;
    }

    private ParticipantRating() { }

    public void AddRatingUpdate(ParticipantRatingUpdate update)
    {
        if (update.RatingId is 0) update.SetRatingRelation(Id);
        _updates.Add(update);
    }
}

