namespace RatingService.Domain.Entities.Ratings;

public class OrganizerRating : RatingBase
{
    public int UserInfoId { get; }
    //public int ExternalUserId { get; }
    public ICollection<EventRating> EventRatings { get; private set; }
    public OrganizerRating(int userInfoId)
    {
        Id = userInfoId;
        UserInfoId = userInfoId;
        //ExternalUserId = externalUserId;
        EventRatings = [];
    }

    private OrganizerRating() { }
}

