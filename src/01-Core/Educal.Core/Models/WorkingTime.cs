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
        public Guid Guid { get; set; } = Guid.NewGuid();
        public EnumDays Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan BookedStartTime { get; set; }
        public TimeSpan BookedEndTime { get; set; }
        public bool IsBooked { get; set; } = false;

        [JsonIgnore]
        public Instructor Instructor { get; set; }
    }
}