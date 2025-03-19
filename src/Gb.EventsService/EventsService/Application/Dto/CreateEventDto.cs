using Common;
using Constants;
using Domain;

namespace Application.Dto;

public class CreateEventDto
{
    public int OrganizerId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime EventDate { get; set; }
    //public int MaxDuration { get; set; }
    public string Location { get; set; }
    //public bool IsClosedParticipation { get; set; }
    public EventStatus EventStatus { get; set; }
    public EventCategory EventCategory { get; set; }
    public int MaxParticipants { get; set; }
    public int MinParticipants { get; set; }
    public IReadOnlyCollection<Participant> Participants { get; set; }
    public bool IsParticipant { get; set; }
    public bool IsOrganizer { get; set; }

    // note: avatar
    //public string? ThemeFile { get; set; }

    //public string? ThemeFileName { get; set; }
}