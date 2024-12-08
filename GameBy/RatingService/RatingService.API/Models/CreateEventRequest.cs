﻿using System.Text.Json.Serialization;

namespace RatingService.API.Models;

public sealed class CreateEventRequest
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("id")]
    public required string EventId { get; set; }
    [JsonPropertyName("created_at")]
    public required DateTime CreationDate { get; set; }
    [JsonPropertyName("category")]
    public required string Category { get; set; }
}