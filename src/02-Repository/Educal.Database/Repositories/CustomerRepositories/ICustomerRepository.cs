using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Educal.Database.Repositories.BaseRepositories;

namespace Educal.Database.Repositories.CustomerRepositories
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer> GetByEmail(string email);
    }
}