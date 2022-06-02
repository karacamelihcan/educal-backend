using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Core.Models;
using Educal.Database.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Educal.Database.Repositories.RegistrarRepositories
{
    public class RegistrarRepository : BaseRepository<Registrar>, IRegistrarRepository
    {
        private readonly EducalDbContext _context;

        public RegistrarRepository(EducalDbContext context)
        {
            _context = context;
        }

        public override async Task AddAsync(Registrar entity)
        {
            await _context.Registrars.AddAsync(entity);
        }

        public override async Task AddRangeAsync(IEnumerable<Registrar> entities)
        {
            await _context.Registrars.AddRangeAsync(entities);
        }

        public override void Delete(Registrar entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public async override Task<IEnumerable<Registrar>> GetAll()
        {
            return await _context.Registrars.Where(rgs => rgs.IsDeleted == false)
                                            .AsNoTracking().ToListAsync();
        }

        public async Task<Registrar> GetByEmail(string email)
        {
            return await _context.Registrars.Where(x => x.Email == email && x.IsDeleted == false).FirstOrDefaultAsync();
        }

        public override async Task<Registrar> GetByGuidAsync(Guid Id)
        {
            return await _context.Registrars.Where(c => c.Guid == Id && c.IsDeleted == false)
                                           .FirstOrDefaultAsync();
        }

        public async override Task<Registrar> GetByIdAsync(int id)
        {
            return await _context.Registrars.Where(c => c.Id == id && c.IsDeleted == false)
                                           .FirstOrDefaultAsync();
        }

        public override void Update(Registrar entity)
        {
            _context.Registrars.Update(entity);
        }

        public override async Task<IQueryable<Registrar>> Where(Expression<Func<Registrar, bool>> expression)
        {
            return _context.Registrars.Where(expression);
        }
    }
}