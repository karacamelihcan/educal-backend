using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educal.Core.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<Instructor> Instructors { get; set; }
    }
}