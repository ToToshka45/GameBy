namespace WebApi.Dto;

public class RefreshTokenResponse
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string AccessToken { get; set; }
    //public string RefreshToken { get; set; }
}
