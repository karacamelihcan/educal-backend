using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Contract.Requests.ManagerRequests;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;

namespace Educal.Services.Services.ManagerServices
{
    public interface IManagerService
    {
        Task<ApiResponse<ManagerDto>> GetByIdAsync(Guid Id);
        Task<ApiResponse<IQueryable<ManagerDto>>> Where(Expression<Func<Manager,bool>> predicate);
        Task<ApiResponse<ManagerDto>> AddAsync(CreateManagerRequest request);
        Task<ApiResponse<NoDataDto>> Remove(Guid Id);
        Task<ApiResponse<ManagerDto>> Update(UpdateManagerRequest request);
        Task<ApiResponse<ManagerDto>> GetByEmailAsync(string Email);
        Task<ApiResponse<List<ManagerDto>>> GetAllAsync();
    }
}