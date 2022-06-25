using System.ComponentModel.DataAnnotations;

namespace ProgSchool.BLL.Models
{
    public class TeacherModel
    {
        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Выберите направление")]
        public int DirectionId { get; set;}
        public int UserId { get; set; }
    }
}
