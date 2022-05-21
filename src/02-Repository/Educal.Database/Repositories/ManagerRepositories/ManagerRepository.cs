using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Core.Models;
using Educal.Database.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Educal.Database.Repositories.ManagerRepositories
{
    public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
    {
        private readonly EducalDbContext _context;

        public ManagerRepository(EducalDbContext context)
        {
            _context = context;
        }

        public override async Task AddAsync(Manager entity)
        {
            await _context.Managers.AddAsync(entity);
        }

        public override async Task AddRangeAsync(IEnumerable<Manager> entities)
        {
            await _context.Managers.AddRangeAsync(entities);
        }

        public override void Delete(Manager entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public async override Task<IEnumerable<Manager>> GetAll()
        {
            return await _context.Managers.AsNoTracking().ToListAsync();
        }

        public async Task<Manager> GetByEmail(string email)
        {
            return await _context.Managers.Where(x => x.Email == email && x.IsDeleted == false).FirstOrDefaultAsync();
        }

        public override async Task<Manager> GetByGuidAsync(Guid Id)
        {
            return await _context.Managers.Where(c => c.Guid == Id && c.IsDeleted == false)
                                           .FirstOrDefaultAsync();
        }

        public async override Task<Manager> GetByIdAsync(int id)
        {
            return await _context.Managers.Where(c => c.Id == id && c.IsDeleted == false)
                                           .FirstOrDefaultAsync();
        }

        public override void Update(Manager entity)
        {
            _context.Managers.Update(entity);
        }

        public override async Task<IQueryable<Manager>> Where(Expression<Func<Manager, bool>> expression)
        {
            return _context.Managers.Where(expression);
        }
    }
}