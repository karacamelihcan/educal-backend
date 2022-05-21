using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Educal.Database.Repositories.BaseRepositories
{
    public abstract class BaseRepository<T> where T : class
    {
        public abstract Task AddAsync(T entity);
        public abstract Task AddRangeAsync(IEnumerable<T> entities);
        public abstract void Delete(T entity);
        public abstract Task<IEnumerable<T>> GetAll();
        public abstract Task<T> GetByIdAsync(int id);
        public abstract void Update(T entity);
        public abstract Task<IQueryable<T>> Where(Expression<Func<T, bool>> expression);
        public abstract Task<T> GetByGuidAsync(Guid Id);

    }
}