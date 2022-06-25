using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProgSchool.BLL.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Введите адрес почты")]
        [EmailAddress(ErrorMessage = "Неверный формат")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
