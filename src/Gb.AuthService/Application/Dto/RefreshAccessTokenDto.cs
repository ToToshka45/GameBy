﻿namespace Application.Dto;

public class RefreshAccessTokenDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
