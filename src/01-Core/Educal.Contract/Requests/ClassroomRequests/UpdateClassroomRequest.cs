using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Enumeration.Enums;

namespace Educal.Contract.Requests.ClassroomRequests
{
    public class UpdateClassroomRequest
    {
        public Guid InstructorId { get; set; }
        public Guid LessonId { get; set; }
        public int Capacity { get; set; }
        public int TotalWeek { get; set; }
        public EnumDays Day { get; set; }
        public int StartTimeHour { get; set; }
        public int StartTimeMinute{ get; set; }
        public int EndTimeHour { get; set; }
        public int EndTimeMinute{ get; set; }
    }
}