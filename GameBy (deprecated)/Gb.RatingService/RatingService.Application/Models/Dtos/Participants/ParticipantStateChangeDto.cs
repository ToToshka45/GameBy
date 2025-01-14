using RatingService.Domain.Enums;

namespace RatingService.Application.Models.Dtos.Participants;

public sealed record ParticipantStateChangeDto(int ExternalParticipantId, ParticipationState State);
