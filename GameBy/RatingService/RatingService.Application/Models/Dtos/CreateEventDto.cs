using RatingService.Domain.Enums;

namespace RatingService.Application.Models.Dtos;

public class CreateEventDto
{
    public string Title { get; set; } = default!;
    public int ExternalEventId { get; set; } = default!;
    public DateTime CreationDate { get; set; }
    public Category Category { get; set; }
}
