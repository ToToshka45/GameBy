using Constants;

namespace Domain
{
    public class Participant : BaseEntity
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        //role
        //public 
        public EventUserRole Role { get; set; }
        public DateTime JoinDate => DateTime.Now;
        public DateTime? LeaveDate { get; set; }
        public bool IsAbsent { get; set; }
    }
}
