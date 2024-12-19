using System.Text.Json.Serialization;

namespace RatingService.API.Models.Users
{
    public class AddUserRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("username")]
        public string UserName { get; set; }
    }
}
