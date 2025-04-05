using Common;
using Constants;

namespace WebApi.Dto;

public class GetShortEventResponse
{
    public int Id { get; set; }
    public int OrganizerId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime EventDate { get; set; }
    public EventCategory EventCategory { get; set; }
    public EventStatus EventStatus { get; set; }
    public bool IsParticipant { get; set; }
    public bool IsOrganizer { get; set; }
    public string? EventAvatarFile { get; set; }
    public string? EventAvatarUrl { get; set; }
    public string? PresignedImageUrl { get; set; }
    public IList<GetParticipantResponse> Participants { get; set; } = [];
}