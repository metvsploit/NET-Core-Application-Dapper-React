using System;
using System.ComponentModel.DataAnnotations;

namespace ProgSchool.BLL.Models
{
    public class SheduleModel
    {
        public int DirectionId { get; set; }
        public int TeacherId { get; set; }
        [Required(ErrorMessage ="Выберите дату и время")]
        public DateTime Date { get; set; }
    }
}
