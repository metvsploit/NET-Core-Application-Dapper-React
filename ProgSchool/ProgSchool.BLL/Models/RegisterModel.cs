using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProgSchool.BLL.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Введите адрес почты")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Введите пароль")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 50 символов")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
