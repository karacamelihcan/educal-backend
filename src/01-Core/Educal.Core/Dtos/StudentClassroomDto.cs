using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Enumeration.Enums;

namespace Educal.Core.Dtos
{
    public class StudentClassroomDto
    {
        public Guid Guid { get; set; }
        public EnumUserRole UserRole { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public EnumStudentStatus StudentStatus { get; set; }
        public string Phone { get; set; }
    }
}