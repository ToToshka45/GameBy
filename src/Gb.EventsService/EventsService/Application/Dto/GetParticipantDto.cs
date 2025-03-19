﻿using Common;

namespace Application.Dto;

public class GetParticipantDto
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
    public string State { get; set; }
}
