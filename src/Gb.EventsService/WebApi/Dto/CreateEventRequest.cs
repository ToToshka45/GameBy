using Common;
using Constants;
using System.Text.Json.Serialization;

namespace WebApi.Dto;

public class CreateEventRequest
{
    public int OrganizerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EventDate { get; set; }
    public string Location { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EventCategory EventCategory { get; set; }
    public int MaxParticipants { get; set; }
    public int MinParticipants { get; set; }
    public IFormFile EventAvatar { get; set; }
}
