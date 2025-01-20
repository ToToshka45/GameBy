using RatingService.Common.Enums;

namespace RatingService.Application.Models.Dtos.Participants;

public sealed record GetParticipantDto(int Id,
                                       int UserId,
                                       int EventId,
                                       ParticipationState State,
                                       float? Rating);