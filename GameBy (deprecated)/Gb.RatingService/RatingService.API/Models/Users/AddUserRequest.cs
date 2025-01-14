using System.Text.Json.Serialization;

namespace RatingService.API.Models.Users;

public sealed class AddUserRequest
{
    [JsonPropertyName("id")]
    public required int ExternalUserId { get; set; }

    [JsonPropertyName("username")]
    public required string UserName { get; set; }
}
