using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Contract.Responses;
using Educal.Core.Dtos;

namespace Educal.Services.Services.BaseServices
{
    public abstract class ServiceBase<TEntity, TDto> where TEntity : class where TDto : class
    {
        public abstract Task<ApiResponse<TDto>> GetByIdAsync(int Id);
        public abstract Task<ApiResponse<IEnumerable<TDto>>> GetAllAsync();
        public abstract Task<ApiResponse<IQueryable<TDto>>> Where(Expression<Func<TEntity,bool>> predicate);
        public abstract Task<ApiResponse<TDto>> AddAsync(TDto entity);
        public abstract Task<ApiResponse<NoDataDto>> Remove(int Id);
        public abstract Task<ApiResponse<NoDataDto>> Update(TDto entity,int Id);
        public abstract Task<ApiResponse<IEnumerable<TDto>>> AddRangeAsync(IEnumerable<TDto> entities);    
        public abstract Task<ApiResponse<TDto>> GetByGuidAsync(Guid Id);
    }
}