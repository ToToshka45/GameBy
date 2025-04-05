namespace WebApi.Dto;

public class RefreshTokenResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string AccessToken { get; set; }
    //public string RefreshToken { get; set; }
}
