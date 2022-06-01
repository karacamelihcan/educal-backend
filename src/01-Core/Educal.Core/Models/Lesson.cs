using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Educal.Core.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;

        [JsonIgnore]
        public List<Instructor> Instructors { get; set; }

        [JsonIgnore]
        public List<Classroom> Classrooms { get; set; }
    }
}