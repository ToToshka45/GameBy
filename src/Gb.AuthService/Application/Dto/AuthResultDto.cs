using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class AuthResultDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsSuccess { get; set; }
    }
}
