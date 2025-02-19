using GameBy.Common.Enums;
using System.Text.Json.Serialization;

namespace GameBy.Common.Models.Requests;

public sealed class FinalizeEventRequest
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("organizer_id")]
    public required int OrganizerId { get; set; }
    [JsonPropertyName("created_at")]
    public required DateTime CreationDate { get; set; }
    [JsonPropertyName("finished_at")]
    public required DateTime FinishedDate { get; set; }
    [JsonPropertyName("category")]
    public required EventCategory Category { get; set; }
    [JsonPropertyName("state")]
    public required EventProgressionState State { get; set; }

    [JsonPropertyName("participants")]
    public required IEnumerable<AddParticipantRequest> Participants { get; set; }
}
