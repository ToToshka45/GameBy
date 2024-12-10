using System.Text.Json.Serialization;

namespace RatingService.API.Models;

public sealed class FinalizeEventRequest
{
    [JsonPropertyName("id")]
    public required int EventId { get; set; }

}
