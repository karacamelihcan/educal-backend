using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Contract.Requests.RegistrarRequests;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;

namespace Educal.Services.Services.RegistrarServices
{
    public interface IRegistrarService
    {
        Task<ApiResponse<RegistrarDto>> GetByIdAsync(Guid Id);
        Task<ApiResponse<IQueryable<RegistrarDto>>> Where(Expression<Func<Registrar,bool>> predicate);
        Task<ApiResponse<RegistrarDto>> AddAsync(CreateRegistrarRequest request);
        Task<ApiResponse<NoDataDto>> Remove(Guid Id);
        Task<ApiResponse<RegistrarDto>> Update(UpdateRegistrarRequest request);
    }
}