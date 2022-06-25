using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProgSchool.BLL.DTO
{
    public class DirectionModel
    {
        [Required(ErrorMessage = "Введите название направления")]
        public string DirectionName { get; set; }
    }
}
