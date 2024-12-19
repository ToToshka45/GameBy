using RatingService.Domain.Entities;

namespace RatingService.Application.Models.Dtos.Users;

public record GetUserRatingsDto(int ExternalUserId, IReadOnlyCollection<Rating> Ratings);
