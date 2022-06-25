using ProgSchool.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgSchool.DAL.Entities
{
    public class User:BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
