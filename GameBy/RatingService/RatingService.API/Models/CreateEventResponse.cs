namespace RatingService.API.Models;

public sealed class CreateEventResponse
{
    public int Id {  get; set; }
    public string Title { get; set; } = default!;
    public int EventId { get; set; }
    public DateTime CreationDate { get; set; }
    public string Category { get; set; } = default!;
    public string State { get; set; } = default!;
}
