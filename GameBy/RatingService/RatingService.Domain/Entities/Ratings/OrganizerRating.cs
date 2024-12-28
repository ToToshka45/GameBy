using RatingService.Domain.Enums;
using RatingService.Domain.Exceptions;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.Entities.Ratings;

public class OrganizerRating : RatingBase
{
    public int ExternalUserId { get; }
    public ICollection<EventRating> EventRatings { get; private set; }
    public OrganizerRating(int externalUserId)
    {
        ExternalUserId = externalUserId;
        EventRatings = [];
    }

    private OrganizerRating() { }
}

