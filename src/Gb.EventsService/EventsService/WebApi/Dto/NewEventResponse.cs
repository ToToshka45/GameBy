using Common;
using Constants;

namespace WebApi.Dto
{
    public class NewEventResponse
    {
        public int Id { get; set; }
        public int OrganizerId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime EventDate { get; set; }

        public string Location { get; set; }

        public bool IsClosedParticipation { get; set; }

        public EventStatus EventStatus { get; set; }

        public EventCategory EventCategory { get; set; }

        public string EventStatusString { get; set; }

        public int ParticipantLimit { get; set; }

        public int ParticipantMinimum { get; set; }
    }
}
