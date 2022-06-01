using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Contract.Requests.ClassroomRequests;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;

namespace Educal.Services.Services.ClassroomServices
{
    public interface IClassroomService
    {
        Task<ApiResponse<ClassroomDto>> GetByIdAsync(Guid Id);
        Task<ApiResponse<IQueryable<ClassroomDto>>> Where(Expression<Func<Classroom,bool>> predicate);
        Task<ApiResponse<ClassroomDto>> AddAsync(CreateClassroomRequest request);
        Task<ApiResponse<NoDataDto>> Remove(Guid Id);
        Task<ApiResponse<ClassroomDto>> UpdateInstructor(UpdateClassInstructorRequest request);
        Task<ApiResponse<ClassroomDto>> Update(UpdateClassroomRequest request);
    }
}