using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Core.Models;
using Educal.Database.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Educal.Database.Repositories.StudentRepositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly EducalDbContext _context;

        public StudentRepository(EducalDbContext context)
        {
            _context = context;
        }

        public override async Task AddAsync(Student entity)
        {
            await _context.Students.AddAsync(entity);
        }

        public override async Task AddRangeAsync(IEnumerable<Student> entities)
        {
            await _context.Students.AddRangeAsync(entities);
        }

        public override void Delete(Student entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public async override Task<IEnumerable<Student>> GetAll()
        {
            return await _context.Students.Where(c => c.IsDeleted == false)
                                          .AsNoTracking()
                                          .Include(std=> std.Classroom)
                                          .Include(std=> std.Classroom.Instructor)
                                          .Include(std=> std.Classroom.Lesson).ToListAsync();
        }

        public async Task<Student> GetByEmail(string email)
        {
            return await _context.Students.Where(x => x.Email == email && x.IsDeleted == false)
                                           .Include(std=> std.Classroom)
                                          .Include(std=> std.Classroom.Instructor)
                                          .Include(std=> std.Classroom.Lesson)
                                          .FirstOrDefaultAsync();
        }

        public override async Task<Student> GetByGuidAsync(Guid Id)
        {
            return await _context.Students.Where(c => c.Guid == Id && c.IsDeleted == false)
                                          .Include(std=> std.Classroom)
                                          .Include(std=> std.Classroom.Instructor)
                                          .Include(std=> std.Classroom.Lesson)
                                          .FirstOrDefaultAsync();
        }

        public async override Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.Where(c => c.Id == id && c.IsDeleted == false)
                                          .Include(std=> std.Classroom)
                                          .FirstOrDefaultAsync();
        }

        public override void Update(Student entity)
        {
            _context.Students.Update(entity);
        }

        public override async Task<IQueryable<Student>> Where(Expression<Func<Student, bool>> expression)
        {
            return _context.Students.Where(expression);
        }
    }
}