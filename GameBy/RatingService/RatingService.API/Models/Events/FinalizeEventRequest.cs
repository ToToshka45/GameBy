using RatingService.API.Models.Participants;
using System.Text.Json.Serialization;

namespace RatingService.API.Models.Events;

public sealed class FinalizeEventRequest
{
    [JsonPropertyName("id")]
    public required int ExternalEventId { get; set; }

    [JsonPropertyName("resolution-state")]
    public required string State { get; set; }

    [JsonPropertyName("participants")]
    public required List<ParticipantStateChangeRequest> Participants { get; set; }
}
