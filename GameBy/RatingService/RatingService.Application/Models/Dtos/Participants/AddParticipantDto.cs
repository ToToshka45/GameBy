using RatingService.Domain.Enums;

namespace RatingService.Application.Models.Dtos.Participants;

public sealed record AddParticipantDto(int ExternalParticipantId,
                                       int ExternalUserId,
                                       int ExternalEventId,
                                       ParticipationState State);