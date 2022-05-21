using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Educal.Database.Repositories.BaseRepositories;

namespace Educal.Database.Repositories.RegistrarRepositories
{
    public interface IRegistrarRepository : IBaseRepository<Registrar>
    {
        Task<Registrar> GetByEmail(string email);
    }
}