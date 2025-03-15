using Common;
using Constants;

namespace WebApi.Dto
{
    public class GetShortEventDto
    {
        public int Id { get; set; }
        public int OrganizerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public EventCategory EventCategory { get; set; }
        public EventStatus EventStatus { get; set; }
        public bool IsUserParticipant {  get; set; }
        public bool IsUserOrganizer { get; set; }
    }
}