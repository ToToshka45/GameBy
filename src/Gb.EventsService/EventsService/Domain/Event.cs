using Common;
using Constants;
using System.Collections.ObjectModel;

namespace Domain
{
    public class Event : BaseEntity
    {
        public int OrganizerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EventDate { get; set; }
        //Hours
        //public int MaxDuration { get; set; }
        public EventCategory EventCategory { get; set; }
        public string Location { get; set; }
        //public bool IsClosedParticipation { get; set; }
        public EventStatus EventStatus { get; set; }
        public int MaxParticipants { get; set; }
        public int MinParticipants { get; set; }
        public virtual IList<EventAction> EventActions { get; set; } = [];
        public virtual IList<Participant> Participants { get; set; } = [];

        //public int? ThemeId { get; set; }
        //public string? ThemeExtension { get; set; }
    }
}
