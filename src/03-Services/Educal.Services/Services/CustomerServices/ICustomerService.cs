using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Contract.Requests.CustomerRequests;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;

namespace Educal.Services.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<ApiResponse<CustomerDto>> GetByIdAsync(Guid Id);
        Task<ApiResponse<IQueryable<CustomerDto>>> Where(Expression<Func<Customer,bool>> predicate);
        Task<ApiResponse<CustomerDto>> AddAsync(CreateCustomerRequest request);
        Task<ApiResponse<NoDataDto>> Remove(Guid Id);
        Task<ApiResponse<CustomerDto>> Update(UpdateCustomerRequest request);
        Task<ApiResponse<CustomerDto>> GetByEmailAsync(string Email);
        Task<ApiResponse<List<CustomerDto>>> GetAllAsync();
    }
}