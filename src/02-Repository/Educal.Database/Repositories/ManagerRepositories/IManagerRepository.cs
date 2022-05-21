using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Educal.Database.Repositories.BaseRepositories;

namespace Educal.Database.Repositories.ManagerRepositories
{
    public interface IManagerRepository : IBaseRepository<Manager>
    {
        Task<Manager> GetByEmail(string email);
    }
}