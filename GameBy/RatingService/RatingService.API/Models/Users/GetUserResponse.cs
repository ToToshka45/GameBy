namespace RatingService.API.Models.Users
{
    public record GetUserResponse(int Id, string UserName, float? GamerRating, float? OrganizerRating);
}
