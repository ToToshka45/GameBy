namespace RatingService.Domain.Entities.Ratings;

public class GamerRating : RatingBase
{
    public int UserInfoId { get; }
    //public int ExternalUserId { get; }
    public ICollection<ParticipantRating> ParticipantRatings { get; private set; }

    public GamerRating(int userInfoId)
    {
        Id = userInfoId;
        UserInfoId = userInfoId;
        //ExternalUserId = externalUserId;
        ParticipantRatings = [];
    }

    private GamerRating() { }
}

