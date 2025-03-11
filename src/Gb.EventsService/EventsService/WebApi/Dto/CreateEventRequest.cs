using Common;
using Constants;

namespace WebApi.Dto;

public class CreateEventRequest
{
    public int OrganizerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime EventDate { get; set; }
    public int MaxDuration { get; set; } // note: max duration of what and why is it int and not TimeSpan?
    public string Location { get; set; } = string.Empty;
    public bool IsClosedParticipation { get; set; } // note: what is it for?
    public EventStatus EventStatus { get; set; } // note: it is not set on creation
    public EventCategory EventCategory { get; set; }
    public int ParticipantLimit { get; set; }
    public int ParticipantMinimum { get; set; }
}
