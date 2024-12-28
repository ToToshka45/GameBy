using RatingService.Domain.Entities;

namespace RatingService.API.Models.Users
{
    public record GetUserRatingsResponse(int ExternalUserId, Rating GamerRating, Rating OrganizerRating);
}
