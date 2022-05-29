using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Educal.Core.Models
{
    public class District
    {
        public int Id { get; set; }
        public string  Name { get; set; }

        [JsonIgnore]
        public City City { get; set; }
    }
}