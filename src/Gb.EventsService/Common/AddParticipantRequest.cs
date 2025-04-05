using System.Text.Json.Serialization;

namespace Common
{
    public class AddParticipantRequest
    {
        [JsonPropertyName("participantId")]
        public int ExternalParticipantId { get; set; }
        [JsonPropertyName("userId")]
        public int ExternalUserId { get; set; }
        [JsonPropertyName("eventId")]
        public int ExternalEventId { get; set; }
    }
}
