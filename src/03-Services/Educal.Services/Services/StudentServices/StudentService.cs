using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Contract.Requests.StudentRequests;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;
using Educal.Database.Repositories.StudentRepositories;
using Educal.Database.UnitOfWorks;
using Educal.Services.MappingProfile;
using Microsoft.Extensions.Logging;

namespace Educal.Services.Services.StudentServices
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _StudentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository StudentService, IUnitOfWork unitOfWork, ILogger<StudentService> logger)
        {
            _StudentService = StudentService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<StudentDto>> AddAsync(CreateStudentRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    return ApiResponse<StudentDto>.Fail("Name section cannot be null", 400);
                }
                if (request.Surname == null)
                {
                    return ApiResponse<StudentDto>.Fail("Surname section cannot be null", 400);
                }
                if (!request.Email.Contains("@") || !request.Email.Contains(".com"))
                {
                    return ApiResponse<StudentDto>.Fail("Please enter a valid email ", 400);
                }
                if (request.Password == null)
                {
                    return ApiResponse<StudentDto>.Fail("Password section cannot be null", 400);
                }
                var checkedUser = _StudentService.GetByEmail(request.Email);
                if(checkedUser != null){
                    return ApiResponse<StudentDto>.Fail("This email is used by a different user",400);
                }

                var user = new Student(){
                    UserRole = Enumeration.Enums.EnumUserRole.Student,
                    Name = request.Name,
                    Surname = request.Surname,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                };
                await _StudentService.AddAsync(user);
                await _unitOfWork.CommitAsync();

                var result = ObjectMapper.Mapper.Map<StudentDto>(user);
                return ApiResponse<StudentDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<StudentDto>.Fail(ex.Message, 500);
            }


        }

        public Task<ApiResponse<IEnumerable<StudentDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<StudentDto>> GetByIdAsync(Guid Id)
        {
            try
            {
                if(Id == Guid.Empty){
                    return ApiResponse<StudentDto>.Fail("Guid Id cannot be null",400);
                }
                var user = await _StudentService.GetByGuidAsync(Id);

                if(user == null){
                    return ApiResponse<StudentDto>.Fail("There is no such a user",404);
                }
                var result = ObjectMapper.Mapper.Map<StudentDto>(user);
                return ApiResponse<StudentDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<StudentDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<NoDataDto>> Remove(Guid Id)
        {
            try
            {
                if(Id == Guid.Empty){
                    return ApiResponse<NoDataDto>.Fail("Guid Id cannot be null",400);
                }
                var user = await _StudentService.GetByGuidAsync(Id);

                if(user == null){
                    return ApiResponse<NoDataDto>.Fail("There is no such a user",404);
                }
                _StudentService.Delete(user);
                await _unitOfWork.CommitAsync();
                return ApiResponse<NoDataDto>.Success(200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<NoDataDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<StudentDto>> Update(UpdateStudentRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    return ApiResponse<StudentDto>.Fail("Name section cannot be null", 400);
                }
                if (request.Surname == null)
                {
                    return ApiResponse<StudentDto>.Fail("Surname section cannot be null", 400);
                }
                if (!request.Email.Contains("@") || !request.Email.Contains(".com"))
                {
                    return ApiResponse<StudentDto>.Fail("Please enter a valid email ", 400);
                }
                if (request.Password == null)
                {
                    return ApiResponse<StudentDto>.Fail("Password section cannot be null", 400);
                }
                var checkedUser = _StudentService.GetByEmail(request.Email);
                if(checkedUser != null){
                    return ApiResponse<StudentDto>.Fail("This email is used by a different user",400);
                }

                var user = new Student(){
                    UserRole = Enumeration.Enums.EnumUserRole.Student,
                    Name = request.Name,
                    Surname = request.Surname,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                };
                _StudentService.Update(user);
                await _unitOfWork.CommitAsync();

                var result = ObjectMapper.Mapper.Map<StudentDto>(user);
                return ApiResponse<StudentDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<StudentDto>.Fail(ex.Message, 500);
            }
        }

        public Task<ApiResponse<IQueryable<StudentDto>>> Where(Expression<Func<Student, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}