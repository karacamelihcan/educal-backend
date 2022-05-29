using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Contract.Requests.LessonRequests;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;

namespace Educal.Services.Services.LessonServices
{
    public interface ILessonService
    {
        Task<ApiResponse<LessonDto>> GetByIdAsync(Guid Id);
        Task<ApiResponse<IQueryable<LessonDto>>> Where(Expression<Func<Lesson,bool>> predicate);
        Task<ApiResponse<LessonDto>> AddAsync(CreateLessonRequest request);
        Task<ApiResponse<NoDataDto>> Remove(Guid Id);
        Task<ApiResponse<LessonDto>> Update(UpdateLessonRequest request);
        Task<ApiResponse<IEnumerable<LessonDto>>> GetLessons();
    }
}