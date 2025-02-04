using Common;
using Constants;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class EventDto
    {
        public int Id { get; set; }

        public int OrganizerId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime EventDate { get; set; }

        public int MaxDuration { get; set; }

        public string Location { get; set; }

        public bool IsClosedParticipation { get; set; }

        public EventStatus EventStatus { get; set; }


        public EventCategory EventCategory { get; set; }

        public int ParticipantLimit { get; set; }

        public int ParticipantMinimum { get; set; }

        public List<EventAction> EventActions { get; set; }

        public List<EventMember> EventMembers { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrMessage { get; set; }
    }
}
