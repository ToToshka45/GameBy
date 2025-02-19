using GameBy.Common.Enums;
using System.Text.Json.Serialization;

namespace GameBy.Common.Models.Requests;

public sealed class AddParticipantRequest
{
    [JsonPropertyName("participantId")]
    public required int ExternalParticipantId { get; set; }
    [JsonPropertyName("userId")]
    public required int ExternalUserId { get; set; }
    //[JsonPropertyName("eventId")]
    //public int ExternalEventId { get; set; }
    [JsonPropertyName("participation_state")]
    public required ParticipationState State { get; set; }
}
