using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Educal.Database.Repositories.BaseRepositories;

namespace Educal.Database.Repositories.InstructorRepositories
{
    public interface IInstructorRepository : IBaseRepository<Instructor>
    {
        Task<Instructor> GetByEmail(string email);
        Task<IQueryable<Instructor>> GetInstructorsAsQueryable();
    }
}