using Domain;

namespace WebApi.Dto
{
    public class RegisterUserResponse:BaseEntity
    {
        public string Username {get; set;}

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
