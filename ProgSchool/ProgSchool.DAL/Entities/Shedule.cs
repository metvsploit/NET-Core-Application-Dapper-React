using System;
using System.Collections.Generic;
using System.Text;

namespace ProgSchool.DAL.Entities
{
    public class Shedule:BaseEntity
    {
        public int DirectionId { get; set; }
        public int TeacherId { get; set; }
        public DateTime Date { get; set; }
    }
}
