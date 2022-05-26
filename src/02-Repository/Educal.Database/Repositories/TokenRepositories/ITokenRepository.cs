using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Educal.Database.Repositories.BaseRepositories;

namespace Educal.Database.Repositories.TokenRepositories
{
    public interface ITokenRepository : IBaseRepository<UserRefreshToken>
    {
        
    }
}