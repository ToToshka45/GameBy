using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common
{
    public class AddParticipantRequest
    {
        [JsonPropertyName("participantId")]
        public  int ExternalParticipantId { get; set; }
        [JsonPropertyName("userId")]
        public  int ExternalUserId { get; set; }
        //[JsonPropertyName("eventId")]
        //public int ExternalEventId { get; set; }
        [JsonPropertyName("participation_state")]
        public  ParticipationState State { get; set; }
    }
}
