namespace Application.Dto;

public class EventsFiltersDto
{
    public string? Title { get; set; }
    public Common.EventCategory[] EventCategories { get; set; } = [];
    public DateTime AfterDate { get; set; }
    public DateTime BeforeDate { get; set; }
    public int? UserId { get; set; }
}
