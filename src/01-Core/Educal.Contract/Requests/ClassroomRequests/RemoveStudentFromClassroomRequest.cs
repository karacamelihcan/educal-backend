using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educal.Contract.Requests.ClassroomRequests
{
    public class RemoveStudentFromClassroomRequest
    {
        public Guid ClassroomId { get; set; }
        public Guid StudentID { get; set; }
    }
}