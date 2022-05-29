using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educal.Contract.Requests.LessonRequests
{
    public class UpdateLessonRequest
    {
        public Guid LessonGuid { get; set; }
        public string Name { get; set; }
    }
}