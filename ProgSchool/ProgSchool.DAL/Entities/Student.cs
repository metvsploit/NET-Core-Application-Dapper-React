using System;
using System.Collections.Generic;
using System.Text;

namespace ProgSchool.DAL.Entities
{
    public class Student:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DirectionId { get; set; }
        public int UserId { get; set; }
    }
}
