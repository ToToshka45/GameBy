using RatingService.Domain.Enums;

namespace RatingService.Application.Models.Dtos.Participants;

public sealed record FinalizeEventDto(int ExternalEventId,
                                      EventProgressionState State,
                                      List<ParticipantStateChangeDto> Participants);
