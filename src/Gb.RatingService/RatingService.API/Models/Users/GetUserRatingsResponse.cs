using RatingService.Domain.Entities.Ratings;

namespace RatingService.API.Models.Users
{
    public record GetUserRatingsResponse(int ExternalUserId, RatingBase GamerRating, RatingBase OrganizerRating);
}
