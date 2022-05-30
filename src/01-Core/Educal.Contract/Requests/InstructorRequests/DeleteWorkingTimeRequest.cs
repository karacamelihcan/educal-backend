using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educal.Contract.Requests.InstructorRequests
{
    public class DeleteWorkingTimeRequest
    {
        public Guid InstructorID { get; set; }
        public Guid TimeGuid { get; set; }
    }
}