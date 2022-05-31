using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Contract.Requests.ClassroomRequests;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;
using Educal.Database.Repositories.ClassroomRepositories;
using Educal.Database.Repositories.InstructorRepositories;
using Educal.Database.Repositories.LessonRepositories;
using Educal.Database.Repositories.StudentRepositories;
using Educal.Database.UnitOfWorks;
using Educal.Services.MappingProfile;
using Microsoft.Extensions.Logging;

namespace Educal.Services.Services.ClassroomServices
{
    public class ClassroomService : IClassroomService
    {
        private readonly IClassroomRepository _classroomRepo;
        private readonly IInstructorRepository _instructorRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly ILessonRepository _lessonRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ClassroomService> _logger;

        public ClassroomService(IClassroomRepository classroomRepo, IInstructorRepository instructorRepo, IStudentRepository studentRepo, ILessonRepository lessonRepo, IUnitOfWork unitOfWork, ILogger<ClassroomService> logger)
        {
            _classroomRepo = classroomRepo;
            _instructorRepo = instructorRepo;
            _studentRepo = studentRepo;
            _lessonRepo = lessonRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<ClassroomDto>> AddAsync(CreateClassroomRequest request)
        {
            try
            {
                var instructor = await _instructorRepo.GetByGuidAsync(request.InstructorId);
                if(instructor == null){
                    return ApiResponse<ClassroomDto>.Fail("There is no such an instructor",404);
                }
                var startTime = new TimeSpan(request.StartTimeHour,0,0);
                var endTime = new TimeSpan(request.EndTimeHour,0,0);
                
                if(startTime > endTime){
                    return ApiResponse<ClassroomDto>.Fail("Please enter valid start and end time ",400);
                } 
                var difference = (endTime-startTime).Hours;
                var selectedTimes = new List<WorkingTime>();
                for (int i = 1; i <= difference ; i++)
                {
                    endTime = TimeSpan.FromHours(startTime.Hours + 1);
                    var time = instructor.WorkingTimes.Where(time => time.StartTime == startTime && time.EndTime == endTime && time.Day == request.Day && time.IsBooked == false).FirstOrDefault();
                    if(time == null){
                        return ApiResponse<ClassroomDto>.Fail("This intructer has not available time for this course",404);
                    }
                    selectedTimes.Add(time);
                    startTime += TimeSpan.FromHours(1);
                }


                if(!selectedTimes.Any()){
                    return ApiResponse<ClassroomDto>.Fail("This Instructer has not available time for this course",400);
                }
                
                var lesson = await _lessonRepo.GetByGuidAsync(request.LessonId);
                if(lesson == null){
                    return ApiResponse<ClassroomDto>.Fail("There is no such a lesson",400);
                }
                var checkinstructorLesson = instructor.Lessons.Where(less => less.Guid == lesson.Guid).Any();
                if(!checkinstructorLesson){
                    return ApiResponse<ClassroomDto>.Fail("This instructor is not suitable for this lesson",400);
                }
                startTime = new TimeSpan(request.StartTimeHour,0,0);
                var startDate = new DateTime(request.StartDate.Year,request.StartDate.Month,request.StartDate.Day,startTime.Hours,startTime.Minutes,startTime.Seconds).ToUniversalTime().AddHours(3);
                var classrom = new Classroom(){
                    Instructor = instructor,
                    Lesson = lesson,
                    Capacity = request.Capacity,
                    WeeklyHour = (endTime - startTime).Hours ,
                    TotalWeek = request.TotalWeek,
                    TotalHour = ((endTime - startTime).Hours)*request.TotalWeek,
                    Day = request.Day,
                    StartDate = startDate,
                    EndDate = startDate.AddDays(7*request.TotalWeek).AddHours((endTime - startTime).Hours).ToUniversalTime(),
                    StartTime = startTime,
                    EndTime = endTime
                };

                await _classroomRepo.AddAsync(classrom);
                
                foreach (var item in selectedTimes)
                {
                    item.IsBooked = true;
                }
                await _unitOfWork.CommitAsync();
                var result = ObjectMapper.Mapper.Map<ClassroomDto>(classrom);

                return ApiResponse<ClassroomDto>.Success(result,200);


            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<ClassroomDto>.Fail(ex.Message,500);
            }
        }

        public Task<ApiResponse<ClassroomDto>> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<NoDataDto>> Remove(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ClassroomDto>> Update(UpdateClassroomRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IQueryable<ClassroomDto>>> Where(Expression<Func<Classroom, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}