using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Enumeration.Enums;

namespace Educal.Core.Models
{
    public class Student : BaseUser
    {
        public bool isHaveEnrolled { get; set; } = true;
        public EnumStudentStatus StudentStatus { get; set; } = EnumStudentStatus.NoRegistration;
        public string Phone { get; set; }
        public Classroom Classroom { get; set; }
    }
}