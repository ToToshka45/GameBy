﻿namespace WebApi.Dto;

public class AddParticipantRequest
{
    public int UserId { get; set; }
    public string Username { get; set; }
    //public string Email { get; set; }
    public DateTime ApplyDate { get; set; }
}
