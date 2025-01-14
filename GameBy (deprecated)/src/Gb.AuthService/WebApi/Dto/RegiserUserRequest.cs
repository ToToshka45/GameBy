﻿using Constants;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto
{
    public class RegiserUserRequest
    {
        //internal UserName Name { get; set; }

        //internal UserPassword Password { get; set; }

        //internal UserEmail Email { get; set; }

        [Required(ErrorMessage ="Введите имя пользователя")]
        [MinLength(3,ErrorMessage ="Слишком короткий логин")]
        [MaxLength(30,ErrorMessage ="Слишком длинный логин")]
        [RegularExpression(RegexPatterns.UserLogin, ErrorMessage = "Только англ буквы и цифры")]
        public string UserName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string UserPassword { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        public bool Validate()
        {
            //Name.Name = UserName;
            return true;
        }
    }
}