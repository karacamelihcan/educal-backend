using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Enumeration.Enums;

namespace Educal.Core.Models
{
    public class Classroom
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public Instructor Instructor { get; set; }
        public List<Student> Students { get; set; }
        public Lesson Lesson { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int WeeklyHour { get; set; }
        public int TotalWeek { get; set; }
        public int TotalHour { get; set; }
        public EnumDays Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsActive { get; set; } = true;

    }
}