using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educal.Core.Models
{
    public class UserRefreshToken
    {
        public Guid Guid { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}