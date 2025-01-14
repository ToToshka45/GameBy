using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Constants;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class UserName
    {
        
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                //NameValidation(value);
                _name = value;
            }
        }

        public UserName(string name)
        {
            NameValidation(name);
        }

        public void NameValidation(string newValue)
        {
            if (newValue.Length < 3 || newValue.Length > 30)
                throw new ValidationException("Length of Name too short or long");
            if (!Regex.IsMatch(newValue,RegexPatterns.UserLogin))
                throw new ValidationException("Use only English alphabet and numbers or _");

            Name = newValue;
        }
    }
}
