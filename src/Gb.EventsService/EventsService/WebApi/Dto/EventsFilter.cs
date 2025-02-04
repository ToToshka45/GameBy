namespace WebApi.Dto
{
    public class EventsFilter
    {
        public string EventTitle { get; set; }

        public Common.EventCategory? EventCategory { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int UserId { get; set; }
    }
}
