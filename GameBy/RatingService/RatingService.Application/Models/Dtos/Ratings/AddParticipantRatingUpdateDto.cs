using RatingService.Domain.Enums;

namespace RatingService.Application.Models.Dtos.Ratings;

public record AddParticipantRatingUpdateDto(float Value, EntityType EntityType, int AuthorId, int SubjectId, int EventId, DateTime CreationDate);
