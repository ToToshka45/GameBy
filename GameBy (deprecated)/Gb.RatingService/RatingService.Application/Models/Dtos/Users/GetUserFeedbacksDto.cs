using RatingService.Domain.Entities;

namespace RatingService.Application.Models.Dtos.Users;

public record GetUserFeedbacksDto(int ExternalUserId,
                                  IReadOnlyCollection<Feedback> GamerFeedbacks,
                                  IReadOnlyCollection<Feedback> OrganizerFeedbacks);
