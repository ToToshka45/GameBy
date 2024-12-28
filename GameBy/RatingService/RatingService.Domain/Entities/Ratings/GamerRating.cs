namespace RatingService.Domain.Entities.Ratings;

public class GamerRating : RatingBase
{
    public int ExternalUserId { get; }
    public ICollection<ParticipantRating> ParticipantRatings { get; private set; }
    public GamerRating(int externalUserId)
    {
        ExternalUserId = externalUserId;
        ParticipantRatings = [];
    }

    private GamerRating() { }
}

