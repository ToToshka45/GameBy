using RatingService.Domain.Entities;

namespace RatingService.API.Models.Users
{
    public record GetUserFeedbacksResponse(int ExternalUserId, IReadOnlyCollection<Feedback> gamerRatings, IReadOnlyCollection<Feedback> organizerRatings);
}
