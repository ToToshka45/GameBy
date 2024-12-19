using System.Text.Json.Serialization;

namespace RatingService.API.Models.Events;

public sealed class CreateEventRequest
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("id")]
    public required int EventId { get; set; }
    [JsonPropertyName("created_at")]
    public required DateTime CreationDate { get; set; }
    [JsonPropertyName("category")]
    public required string Category { get; set; }
    [JsonPropertyName("state")]
    public required string State { get; set; }
}
