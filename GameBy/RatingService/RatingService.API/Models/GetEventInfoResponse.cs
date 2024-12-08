using RatingService.Domain.Enums;

namespace RatingService.API.Models;

public sealed class GetEventInfoResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ExternalEventId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Category { get; set; }

    public float Rating { get; set; }

}
