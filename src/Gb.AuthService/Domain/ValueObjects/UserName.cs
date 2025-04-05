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
        public string Name { get; private set; }

        public UserName(string name)
        {
            NameValidation(name);
            Name = name;
        }

        public void NameValidation(string newValue)
        {
            if (newValue.Length < 3 || newValue.Length > 30)
                throw new ValidationException("Length of Name too short or long");
            if (!Regex.IsMatch(newValue,RegexPatterns.UserLogin))
                throw new ValidationException("Use only English alphabet and numbers or _");
        }
    }
}
