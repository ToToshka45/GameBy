using RatingService.Domain.Entities.Ratings;

namespace RatingService.Application.Models.Dtos.Users;

public record GetUserRatingsDto(int ExternalUserId, RatingBase GamerRating, RatingBase OrganizerRating);
