using RatingService.Domain.Enums;
using System.Text.Json.Serialization;

namespace RatingService.Application.Models.Dtos.Participants;

public sealed class FinalizeEventDto()
{
    public required string Title { get; set; }
    public required int EventId { get; set; }
    public required int OrganizerId { get; set; }
    public required DateTime CreationDate { get; set; }
    public required DateTime FinishedDate { get; set; }
    public required EventCategory Category { get; set; }
    public required EventProgressionState State { get; set; }
    public required IEnumerable<AddParticipantDto> Participants { get; set; }
}
