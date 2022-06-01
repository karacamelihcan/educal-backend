using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Enumeration.Enums;

namespace Educal.Core.Dtos
{
    public class ClassroomDto
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public InstructorClassDto Instructor { get; set; }
        public List<StudentDto> Students { get; set; }
        public LessonDto Lesson { get; set; }
        public int Capacity { get; set; }
        public int WeeklyHour { get; set; }
        public int TotalHour { get; set; }
        public EnumDays Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}