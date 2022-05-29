using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Contract.Requests.LessonRequests;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;
using Educal.Database.Repositories.LessonRepositories;
using Educal.Database.UnitOfWorks;
using Educal.Services.MappingProfile;
using Microsoft.Extensions.Logging;

namespace Educal.Services.Services.LessonServices
{
    public class LessonService : ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILessonRepository _lessonRepository;
        private readonly ILogger<LessonService> _logger;

        public LessonService(IUnitOfWork unitOfWork, ILessonRepository lessonRepository, ILogger<LessonService> logger)
        {
            _unitOfWork = unitOfWork;
            _lessonRepository = lessonRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<LessonDto>> AddAsync(CreateLessonRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    return ApiResponse<LessonDto>.Fail("Lesson name field cannot be null", 400);
                }

                var checkLesson = await _lessonRepository.GetLessonByName(request.Name);
                if (checkLesson != null)
                {
                    return ApiResponse<LessonDto>.Fail("There is a same name lesson already", 400);
                }
                var lesson = new Lesson(){
                    Name = request.Name
                };

                await _lessonRepository.AddAsync(lesson);
                await _unitOfWork.CommitAsync();
                var result = ObjectMapper.Mapper.Map<LessonDto>(lesson);

                return ApiResponse<LessonDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<LessonDto>.Fail(ex.Message,500);
            }

        }

        public async Task<ApiResponse<LessonDto>> GetByIdAsync(Guid Id)
        {
            try
            {
                if(Id == Guid.Empty){
                    return ApiResponse<LessonDto>.Fail("Id field cannot be null", 400);
                }
                var lesson = await _lessonRepository.GetByGuidAsync(Id);
                if(lesson == null){
                    return ApiResponse<LessonDto>.Fail("There is no such a lesson", 404);
                }
                var result = ObjectMapper.Mapper.Map<LessonDto>(lesson);
                return ApiResponse<LessonDto>.Success(result,200);

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<LessonDto>.Fail(ex.Message,500);
            }
        }

        public async Task<ApiResponse<IEnumerable<LessonDto>>> GetLessons()
        {
            try
            {
                var lessons = await _lessonRepository.GetAll();
                if(lessons == null && !lessons.Any()){
                    return ApiResponse<IEnumerable<LessonDto>>.Fail("There is no any lesson",404);
                }
                var result = new List<LessonDto>();
                foreach (var les in lessons)
                {
                    result.Add(ObjectMapper.Mapper.Map<LessonDto>(les));
                }
                return ApiResponse<IEnumerable<LessonDto>>.Success(result,200);

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<IEnumerable<LessonDto>>.Fail(ex.Message,500);
            }
        }

        public async Task<ApiResponse<NoDataDto>> Remove(Guid Id)
        {
            try
            {
                if(Id == Guid.Empty){
                    return ApiResponse<NoDataDto>.Fail("Id field cannot be null", 400);
                }
                var lesson = await _lessonRepository.GetByGuidAsync(Id);
                if(lesson == null){
                    return ApiResponse<NoDataDto>.Fail("There is no such a lesson", 404);
                }
                _lessonRepository.Delete(lesson);
                await _unitOfWork.CommitAsync();
                return ApiResponse<NoDataDto>.Success(200);

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<NoDataDto>.Fail(ex.Message,500);
            }
        }

        public async Task<ApiResponse<LessonDto>> Update(UpdateLessonRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    return ApiResponse<LessonDto>.Fail("Lesson name field cannot be null", 400);
                }
                if(request.LessonGuid == Guid.Empty){
                    return ApiResponse<LessonDto>.Fail("Id field cannot be null", 400);
                }

                var checkLesson = await _lessonRepository.GetByGuidAsync(request.LessonGuid);
                if (checkLesson == null)
                {
                    return ApiResponse<LessonDto>.Fail("There is no such a lesson", 404);
                }

                var checkName = await _lessonRepository.GetLessonByName(request.Name);
                if (checkName != null)
                {
                    return ApiResponse<LessonDto>.Fail("There is a same name lesson already", 400);
                }
                checkLesson.Name = request.Name;

                _lessonRepository.Update(checkLesson);
                await _unitOfWork.CommitAsync();
                var result = ObjectMapper.Mapper.Map<LessonDto>(checkLesson);

                return ApiResponse<LessonDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<LessonDto>.Fail(ex.Message,500);
            }
        }

        public Task<ApiResponse<IQueryable<LessonDto>>> Where(Expression<Func<Lesson, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}