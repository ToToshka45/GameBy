using Domain;

namespace WebApi.Dto
{
    public class NewUserResponse:BaseEntity
    {
        public string UserName {get; set;}

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
