using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class UserEmail
    {
        public UserEmail(string email) { 
            Email = email;
            //throw new ValidationException("test");
        }
        public string Email { get; set; }
    }
}
