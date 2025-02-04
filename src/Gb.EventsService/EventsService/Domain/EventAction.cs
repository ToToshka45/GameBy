using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class EventAction:BaseEntity
    {
        public int EventId { get; set; }

        public int ParticipantId { get; set; }

        public EventType EventType { get; set; }

        public DateTime CreationDate = DateTime.Now;

        public string PublicText { get; set; }


    }
}
