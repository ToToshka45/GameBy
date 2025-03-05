using Newtonsoft.Json;

namespace WebApi.Dto
{
    public class SimpleLoginDto
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
