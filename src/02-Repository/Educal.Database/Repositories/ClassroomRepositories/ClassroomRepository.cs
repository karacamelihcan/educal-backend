using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Core.Models;
using Educal.Database.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Educal.Database.Repositories.ClassroomRepositories
{
    public class ClassroomRepository : BaseRepository<Classroom> , IClassroomRepository
    {
        private readonly EducalDbContext _context;

        public ClassroomRepository(EducalDbContext context)
        {
            _context = context;
        }

        public override async Task AddAsync(Classroom entity)
        {
            await _context.Classrooms.AddAsync(entity);
        }

        public override async Task AddRangeAsync(IEnumerable<Classroom> entities)
        {
            await _context.Classrooms.AddRangeAsync(entities);
        }

        public override void Delete(Classroom entity)
        {
            entity.IsActive = false;
            Update(entity);
        }

        public override async Task<IEnumerable<Classroom>> GetAll()
        {
            return await _context.Classrooms.Where(cls => cls.IsActive == true)
                                            .Include(cls => cls.Instructor)
                                            .Include(cls => cls.Students)
                                            .Include(cls => cls.Lesson)
                                            .AsNoTracking()
                                            .ToListAsync();
        }

        public override async Task<Classroom> GetByGuidAsync(Guid Id)
        {
            return await _context.Classrooms.Where(cls => cls.IsActive == true && cls.Guid == Id)
                                            .Include(cls => cls.Instructor)
                                            .Include(cls => cls.Students)
                                            .Include(cls => cls.Lesson)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync();
        }

        public override async Task<Classroom> GetByIdAsync(int id)
        {
            return await _context.Classrooms.Where(cls => cls.IsActive == true && cls.Id == id)
                                            .Include(cls => cls.Instructor)
                                            .Include(cls => cls.Students)
                                            .Include(cls => cls.Lesson)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync();
        }

        public override void Update(Classroom entity)
        {
            _context.Classrooms.Update(entity);
        }

        public override async Task<IQueryable<Classroom>> Where(Expression<Func<Classroom, bool>> expression)
        {
            return _context.Classrooms.Where(expression);
        }
    }
}