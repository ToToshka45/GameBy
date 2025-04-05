using Domain;

namespace WebApi.Dto
{
    public class ValidateTokenResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Role[] Roles { get; set; }
    }
}
