using RatingService.Domain.Enums;

namespace RatingService.API.Models;

public sealed record GetParticipantResponse(int Id, int UserId, int EventId, string State, float? Rating);

