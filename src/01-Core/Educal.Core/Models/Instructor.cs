using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educal.Core.Models
{
    public class Instructor : BaseUser
    {
        public List<WorkingTime> WorkingTimes { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Classroom> Classrooms { get; set; }
        
    }
}