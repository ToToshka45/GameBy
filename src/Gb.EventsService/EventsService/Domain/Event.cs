using Common;
using Constants;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Event:BaseEntity
    {
        
        public int OrganizerId { get; set; }

        public string OrganizerName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime EventDate { get; set; }

        //Hours
        public int MaxDuration { get; set; }

        public EventCategory EventCategory { get; set; }

        public string Location { get; set; }

        public bool IsClosedParticipation { get; set; }

        public EventStatus EventStatus { get; set; }

        public int ParticipantLimit { get; set; }

        public int ParticipantMinimum { get; set; }

        public virtual Collection<EventAction> EventActions { get; set; }

        public virtual Collection<EventMember> EventMembers { get; set; }

        
    }
}
