using Common;

namespace Domain;

public class Participant : BaseEntity
{
    public int EventId { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; } 
    //role
    //public 
    //public EventUserRole Role { get; set; }
    public DateTime ApplyDate { get; set; }
    public DateTime AcceptedDate { get; set; }
    public DateTime? LeaveDate { get; set; }
    public ParticipationState State { get; set; }
    //public bool IsAbsent { get; set; }
}
