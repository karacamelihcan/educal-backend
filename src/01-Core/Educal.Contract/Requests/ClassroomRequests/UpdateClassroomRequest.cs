using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Enumeration.Enums;

namespace Educal.Contract.Requests.ClassroomRequests
{
    public class UpdateClassroomRequest
    {
        public Guid LessonId { get; set; }
        public int Capacity { get; set; }
        public EnumDays Day { get; set; }
        public int StartTimeHour { get; set; }
        public int EndTimeHour { get; set; }
    }
}