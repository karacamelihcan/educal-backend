using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Contract.Requests.InstructorRequests;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;

namespace Educal.Services.Services.InstructorServices
{
    public interface IInstructorService
    {
        Task<ApiResponse<InstructorDto>> GetByIdAsync(Guid Id);
        Task<ApiResponse<IQueryable<InstructorDto>>> Where(Expression<Func<Instructor,bool>> predicate);
        Task<ApiResponse<InstructorDto>> AddAsync(CreateInstructorRequest request);
        Task<ApiResponse<NoDataDto>> Remove(Guid Id);
        Task<ApiResponse<InstructorDto>> Update(UpdateInstructorRequest request);
        Task<ApiResponse<InstructorDto>> AddWorkingTime(AddWorkingTimeRequest request);
    }
}