using System.Text.Json.Serialization;

namespace RatingService.API.Models.Participants;

public sealed class ParticipantStateChangeRequest
{
    [JsonPropertyName("participantId")]
    public required int ExternalParticipantId { get; set; }

    [JsonPropertyName("state-change")]
    public required string State { get; set; }
}
