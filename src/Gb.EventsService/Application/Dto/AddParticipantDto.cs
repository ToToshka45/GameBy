namespace Application.Dto
{
    public class AddParticipantDto
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }

        //role
        //public 
        //public EventUserRole Role { get; set; }

        public DateTime ApplyDate { get; set; }

        //public bool IsSuccess { get; set; }
        //public string ErrMessage { get; set; }
    }
}
