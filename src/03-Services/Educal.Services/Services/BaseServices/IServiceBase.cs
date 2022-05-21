using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Contract.Responses;
using Educal.Core.Dtos;

namespace Educal.Services.Services.BaseServices
{
    public interface IServiceBase<TEntity,TDto> where TEntity: class where TDto : class
    {
        Task<ApiResponse<TDto>> GetByIdAsync(int Id);
        Task<ApiResponse<IEnumerable<TDto>>> GetAllAsync();
        Task<ApiResponse<IQueryable<TDto>>> Where(Expression<Func<TEntity,bool>> predicate);
        Task<ApiResponse<TDto>> AddAsync(TDto entity);
        Task<ApiResponse<NoDataDto>> Remove(int Id);
        Task<ApiResponse<NoDataDto>> Update(TDto entity,int Id);
        Task<ApiResponse<IEnumerable<TDto>>> AddRangeAsync(IEnumerable<TDto> entities);
        Task<ApiResponse<TDto>> GetByGuidAsync(Guid Id);
    
    }
}