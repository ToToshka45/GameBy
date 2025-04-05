using Domain;

namespace WebApi.Dto
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Role[] Roles { get; set; }
    }
}
