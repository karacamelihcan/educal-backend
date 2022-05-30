using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educal.Contract.Requests.InstructorRequests
{
    public class DeleteLessonRequest
    {
        public Guid InstructorId { get; set; }
        public Guid LessonId { get; set; }
    }
}