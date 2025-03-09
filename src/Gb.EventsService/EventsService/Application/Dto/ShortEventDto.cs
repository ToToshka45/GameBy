using Common;
using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class ShortEventDto
    {
        public int Id { get; set; }

        public int OrganizerId { get; set; }

        public string OrganizerName {get;set;}

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        public EventCategory EventCategory { get; set; }

        public EventStatus EventStatus { get; set; }

        public bool IsUserParticipated {  get; set; }

        public ParticipationState? UserParticipationState {get;set;}

        public bool IsUserOrganizer { get; set; }
    }
}
