using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common
{
    public class FinalizeEventRequest
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("organizer_id")]
        public int OrganizerId { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreationDate { get; set; }
        [JsonPropertyName("finished_at")]
        public DateTime FinishedDate { get; set; }
        [JsonPropertyName("category")]
        public EventCategory Category { get; set; }
        [JsonPropertyName("state")]
        public EventProgressionState State { get; set; }

        [JsonPropertyName("participants")]
        public IEnumerable<AddParticipantRequest> Participants { get; set; }
    }
}
