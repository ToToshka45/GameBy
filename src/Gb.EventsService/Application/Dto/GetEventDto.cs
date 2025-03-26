using Common;
using Constants;

namespace Application.Dto;

public class GetEventDto
{
    public int Id { get; set; }
    public int OrganizerId { get; set; }
    public string? EventAvatarUrl { get; set; }
    public EventAvatarFile? EventAvatarFile { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime EventDate { get; set; }
    //public int MaxDuration { get; set; }
    public string? Location { get; set; }
    //public bool IsClosedParticipation { get; set; }
    public EventStatus EventStatus { get; set; }
    public EventCategory EventCategory { get; set; }
    public int MaxParticipants { get; set; }
    public int MinParticipants { get; set; }
    //public List<EventAction> EventActions { get; set; }
    public IList<GetParticipantDto> Participants { get; set; } = [];
    public bool IsParticipant { get; set; }
    public bool IsOrganizer { get; set; }
    //public bool IsSuccess { get; set; }
    //public string ErrMessage { get; set; }
}

public sealed class EventAvatarFile
{
    public string? ContentType { get; set; }
    public string? Content {  get; set; }
}
