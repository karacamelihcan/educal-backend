using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Educal.Contract.Requests.InstructorRequests;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;
using Educal.Database.Repositories.InstructorRepositories;
using Educal.Database.UnitOfWorks;
using Educal.Services.MappingProfile;
using Microsoft.Extensions.Logging;

namespace Educal.Services.Services.InstructorServices
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _InstructorService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<InstructorService> _logger;
        private readonly SHA256 _passwordHasher = new SHA256CryptoServiceProvider();

        public InstructorService(IInstructorRepository InstructorService, IUnitOfWork unitOfWork, ILogger<InstructorService> logger)
        {
            _InstructorService = InstructorService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<InstructorDto>> AddAsync(CreateInstructorRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    return ApiResponse<InstructorDto>.Fail("Name section cannot be null", 400);
                }
                if (request.Surname == null)
                {
                    return ApiResponse<InstructorDto>.Fail("Surname section cannot be null", 400);
                }
                if (!request.Email.Contains("@") || !request.Email.Contains(".com"))
                {
                    return ApiResponse<InstructorDto>.Fail("Please enter a valid email ", 400);
                }
                if (request.Password == null)
                {
                    return ApiResponse<InstructorDto>.Fail("Password section cannot be null", 400);
                }
                var checkedUser = _InstructorService.GetByEmail(request.Email);
                if(checkedUser != null){
                    return ApiResponse<InstructorDto>.Fail("This email is used by a different user",400);
                }

                var user = new Instructor(){
                    UserRole = Enumeration.Enums.EnumUserRole.Instructor,
                    Name = request.Name,
                    Surname = request.Surname,
                    Email = request.Email,
                    Password = Convert.ToBase64String(_passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(request.Password))),
                };
                await _InstructorService.AddAsync(user);
                await _unitOfWork.CommitAsync();

                var result = ObjectMapper.Mapper.Map<InstructorDto>(user);
                return ApiResponse<InstructorDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<InstructorDto>.Fail(ex.Message, 500);
            }


        }

        public Task<ApiResponse<IEnumerable<InstructorDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<InstructorDto>> GetByIdAsync(Guid Id)
        {
            try
            {
                if(Id == Guid.Empty){
                    return ApiResponse<InstructorDto>.Fail("Guid Id cannot be null",400);
                }
                var user = await _InstructorService.GetByGuidAsync(Id);

                if(user == null){
                    return ApiResponse<InstructorDto>.Fail("There is no such a user",404);
                }
                var result = ObjectMapper.Mapper.Map<InstructorDto>(user);
                return ApiResponse<InstructorDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<InstructorDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<NoDataDto>> Remove(Guid Id)
        {
            try
            {
                if(Id == Guid.Empty){
                    return ApiResponse<NoDataDto>.Fail("Guid Id cannot be null",400);
                }
                var user = await _InstructorService.GetByGuidAsync(Id);

                if(user == null){
                    return ApiResponse<NoDataDto>.Fail("There is no such a user",404);
                }
                _InstructorService.Delete(user);
                await _unitOfWork.CommitAsync();
                return ApiResponse<NoDataDto>.Success(200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<NoDataDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<InstructorDto>> Update(UpdateInstructorRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    return ApiResponse<InstructorDto>.Fail("Name section cannot be null", 400);
                }
                if (request.Surname == null)
                {
                    return ApiResponse<InstructorDto>.Fail("Surname section cannot be null", 400);
                }
                if (!request.Email.Contains("@") || !request.Email.Contains(".com"))
                {
                    return ApiResponse<InstructorDto>.Fail("Please enter a valid email ", 400);
                }
                if (request.Password == null)
                {
                    return ApiResponse<InstructorDto>.Fail("Password section cannot be null", 400);
                }
                var user = await _InstructorService.GetByGuidAsync(request.UserId);
                
                if(user == null){
                    return ApiResponse<InstructorDto>.Fail("There is no such a user",404);
                }
                user.Name = request.Name;
                user.Surname = request.Surname;
                user.Email = request.Email;
                user.Password = Convert.ToBase64String(_passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(request.Password)));
                _InstructorService.Update(user);
                await _unitOfWork.CommitAsync();

                var result = ObjectMapper.Mapper.Map<InstructorDto>(user);
                return ApiResponse<InstructorDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<InstructorDto>.Fail(ex.Message, 500);
            }
        }

        public Task<ApiResponse<IQueryable<InstructorDto>>> Where(Expression<Func<Instructor, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}