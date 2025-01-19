namespace RatingService.API.Models.Users
{
    public record GetUserInfoResponse(int Id, string UserName, float? GamerRating, float? OrganizerRating);
}
