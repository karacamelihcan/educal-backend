using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Educal.Enumeration.Enums;

namespace Educal.Core.Models
{
    public class WorkingTime
    {
        public int Id { get; set; }
        public EnumDays Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public TimeOnly BookedStartTime { get; set; }
        public TimeOnly BookedEndTime { get; set; }
        public bool IsBooked { get; set; } = false;

        [JsonIgnore]
        public Instructor Instructor { get; set; }
    }
}