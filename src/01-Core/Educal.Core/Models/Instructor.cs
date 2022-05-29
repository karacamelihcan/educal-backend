using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educal.Core.Models
{
    public class Instructor : BaseUser
    {
        public List<WorkingTime> WorkingTimes { get; set; }
    }
}