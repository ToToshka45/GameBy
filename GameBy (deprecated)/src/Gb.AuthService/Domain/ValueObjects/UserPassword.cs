using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class UserPassword
    {
        public string Password { get; set; }

        public UserPassword(string password)
        {
            Password = password;
        }
    }
}
