namespace RatingService.Application.Models.Dtos.Users;

public record GetUserInfoDto(int Id, string Username, float? GamerRating, float? OrganizerRating);
