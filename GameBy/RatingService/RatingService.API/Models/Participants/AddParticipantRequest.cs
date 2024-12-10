using System.Text.Json.Serialization;

namespace RatingService.API.Models;

public sealed class AddParticipantRequest
{
    [JsonPropertyName("participantId")]
    public required int ParticipantId { get; set; }
    [JsonPropertyName("userId")]
    public required int UserId { get; set; }
    [JsonPropertyName("eventId")]
    public required int EventId { get; set; }
    [JsonPropertyName("participation-state")]
    public required string ParticipationState { get; set; }
    [JsonPropertyName("state")]
    public required string State { get; set; }
}
