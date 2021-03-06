using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Enumeration.Enums;

namespace Educal.Contract.Requests.InstructorRequests
{
    public class GetInstructorByTimeQueryRequest
    {
        public EnumDays Day { get; set; }
        public int StartTimeHour { get; set; }
        public int StartTimeMinute{ get; set; }
        public int EndTimeHour { get; set; }
        public int EndTimeMinute { get; set; }
    }
}