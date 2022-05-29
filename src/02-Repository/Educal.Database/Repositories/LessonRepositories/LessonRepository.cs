using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Core.Models;
using Educal.Database.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Educal.Database.Repositories.LessonRepositories
{
    public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
    {
        private readonly EducalDbContext _contex;

        public LessonRepository(EducalDbContext contex)
        {
            _contex = contex;
        }

        public override async Task AddAsync(Lesson entity)
        {
            await _contex.Lessons.AddAsync(entity);
        }

        public override async Task AddRangeAsync(IEnumerable<Lesson> entities)
        {
            await _contex.Lessons.AddRangeAsync(entities);
        }

        public override void Delete(Lesson entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public override async Task<IEnumerable<Lesson>> GetAll()
        {
            return await _contex.Lessons.Where(les => les.IsDeleted == false).AsNoTracking().ToListAsync();
        }

        public override async Task<Lesson> GetByGuidAsync(Guid Id)
        {
            return await _contex.Lessons.Where(les => les.Guid == Id && les.IsDeleted == false).FirstOrDefaultAsync();
        }

        public override async Task<Lesson> GetByIdAsync(int id)
        {
            return await _contex.Lessons.Where(les => les.Id == id && les.IsDeleted == false).FirstOrDefaultAsync();
        }

        public async Task<Lesson> GetLessonByContainName(string LessonName)
        {
            return await _contex.Lessons.Where(les => les.Name.Contains(LessonName)).FirstOrDefaultAsync();
        }

        public async Task<Lesson> GetLessonByName(string Name)
        {
            return await _contex.Lessons.Where(les => les.Name == Name).FirstOrDefaultAsync();
        }

        public override void Update(Lesson entity)
        {
            _contex.Lessons.Update(entity);
        }

        public override async Task<IQueryable<Lesson>> Where(Expression<Func<Lesson, bool>> expression)
        {
            return _contex.Lessons.Where(expression);
        }
    }
}