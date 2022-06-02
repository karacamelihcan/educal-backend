using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Core.Models;
using Educal.Database.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Educal.Database.Repositories.InstructorRepositories
{
    public class InstructorRepository : BaseRepository<Instructor>, IInstructorRepository
    {
        private readonly EducalDbContext _context;

        public InstructorRepository(EducalDbContext context)
        {
            _context = context;
        }

        public override async Task AddAsync(Instructor entity)
        {
            await _context.Instructors.AddAsync(entity);
        }

        public override async Task AddRangeAsync(IEnumerable<Instructor> entities)
        {
            await _context.Instructors.AddRangeAsync(entities);
        }

        public override void Delete(Instructor entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public async override Task<IEnumerable<Instructor>> GetAll()
        {
            return await _context.Instructors.AsNoTracking()
                                             .Where(x => x.IsDeleted == false)
                                             .Include(x => x.WorkingTimes)
                                             .Include(inst => inst.Lessons)
                                             .Include(inst => inst.Classrooms)
                                             .AsNoTracking()
                                             .ToListAsync();
        }

        public async Task<Instructor> GetByEmail(string email)
        {
            return await _context.Instructors.Where(x => x.Email == email && x.IsDeleted == false)
                                             .Include(x => x.WorkingTimes)
                                             .Include(inst => inst.Lessons)
                                             .Include(inst => inst.Classrooms)
                                             .FirstOrDefaultAsync();
        }

        public override async Task<Instructor> GetByGuidAsync(Guid Id)
        {
            return await _context.Instructors.Where(c => c.Guid == Id && c.IsDeleted == false)
                                             .Include(x => x.WorkingTimes)
                                             .Include(inst => inst.Lessons)
                                             .Include(inst => inst.Classrooms)   
                                             .FirstOrDefaultAsync();
        }

        public async override Task<Instructor> GetByIdAsync(int id)
        {
            return await _context.Instructors.Where(c => c.Id == id && c.IsDeleted == false)
                                             .Include(x => x.WorkingTimes)
                                             .Include(inst => inst.Lessons)
                                             .Include(inst => inst.Classrooms)
                                             .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<Instructor>> GetInstructorsAsQueryable()
        {
            return _context.Instructors.Where(inst => inst.IsDeleted == false)
                                       .Include(inst => inst.WorkingTimes)
                                       .Include(inst => inst.Lessons)
                                       .Include(inst => inst.Classrooms)
                                       .AsQueryable();
                                       

        }

        public override void Update(Instructor entity)
        {
            _context.Instructors.Update(entity);
        }

        public override async Task<IQueryable<Instructor>> Where(Expression<Func<Instructor, bool>> expression)
        {
            return _context.Instructors.Where(expression);
        }
    }
}