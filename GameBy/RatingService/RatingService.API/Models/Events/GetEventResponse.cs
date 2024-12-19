using RatingService.Domain.Enums;

namespace RatingService.API.Models.Events;

public sealed class GetEventResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public int ExternalEventId { get; set; }
    public DateTime CreationDate { get; set; }
    public string Category { get; set; } = default!;

    public float Rating { get; set; }

}
