using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Contract.Requests.StudentRequests;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;

namespace Educal.Services.Services.StudentServices
{
    public interface IStudentService
    {
        Task<ApiResponse<StudentDto>> GetByIdAsync(Guid Id);
        Task<ApiResponse<IQueryable<StudentDto>>> Where(Expression<Func<Student,bool>> predicate);
        Task<ApiResponse<StudentDto>> AddAsync(CreateStudentRequest request);
        Task<ApiResponse<NoDataDto>> Remove(Guid Id);
        Task<ApiResponse<StudentDto>> Update(UpdateStudentRequest request);
    }
}