using System;

namespace ProgSchool.DAL.DTO
{
    public class SheduleDto
    {
        public int Id { get; set; }
        public string DirectionName { get; set; }
        public string TeacherName { get; set; }
        public DateTime DateTime { get; set; }
    }
}
