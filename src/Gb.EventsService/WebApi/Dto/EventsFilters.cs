namespace WebApi.Dto
{
    public class EventsFilters
    {
        public string? Title { get; set; }
        public Common.EventCategory[] EventCategories { get; set; } = [];
        public DateTime AfterDate { get; set; }
        public DateTime BeforeDate { get; set; }
        public int? UserId { get; set; }
    }
}
