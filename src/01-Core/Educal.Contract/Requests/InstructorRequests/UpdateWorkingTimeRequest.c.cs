using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Enumeration.Enums;

namespace Educal.Contract.Requests.InstructorRequests
{
    public class UpdateWorkingTimeRequest
    {
        public Guid InstructorID { get; set; }
        public Guid TimeGuid { get; set; }
        public EnumDays Day { get; set; }
        public int StartTimeHour { get; set; }
        public int StartTimeMinute{ get; set; }
        public int EndTimeHour { get; set; }
        public int EndTimeMinute { get; set; }
    }
}