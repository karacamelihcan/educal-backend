using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Enumeration.Enums;

namespace Educal.Core.Models
{
    public class Student : BaseUser
    {
        public bool isHaveEnrolled { get; set; }
        public EnumStudentStatus StudentStatus { get; set; }
        public string Phone { get; set; }
    }
}