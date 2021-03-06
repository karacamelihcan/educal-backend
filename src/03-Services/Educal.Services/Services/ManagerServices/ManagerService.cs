using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Educal.Contract.Requests.ManagerRequests;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;
using Educal.Database.Repositories.ManagerRepositories;
using Educal.Database.UnitOfWorks;
using Educal.Services.MappingProfile;
using Microsoft.Extensions.Logging;

namespace Educal.Services.Services.ManagerServices
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _ManagerService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ManagerService> _logger;
                
        private readonly SHA256 _passwordHasher = new SHA256CryptoServiceProvider();

        public ManagerService(IManagerRepository ManagerService, IUnitOfWork unitOfWork, ILogger<ManagerService> logger)
        {
            _ManagerService = ManagerService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<ManagerDto>> AddAsync(CreateManagerRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    return ApiResponse<ManagerDto>.Fail("Name section cannot be null", 400);
                }
                if (request.Surname == null)
                {
                    return ApiResponse<ManagerDto>.Fail("Surname section cannot be null", 400);
                }
                if (!request.Email.Contains("@") || !request.Email.Contains(".com"))
                {
                    return ApiResponse<ManagerDto>.Fail("Please enter a valid email ", 400);
                }
                if (request.Password == null)
                {
                    return ApiResponse<ManagerDto>.Fail("Password section cannot be null", 400);
                }
                var checkedUser = await _ManagerService.GetByEmail(request.Email);
                if(checkedUser != null){
                    return ApiResponse<ManagerDto>.Fail("This email is used by a different user",400);
                }

                var user = new Manager(){
                    UserRole = Enumeration.Enums.EnumUserRole.Manager,
                    Name = request.Name,
                    Surname = request.Surname,
                    Email = request.Email,
                    Password = Convert.ToBase64String(_passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(request.Password)))
                };

                await _ManagerService.AddAsync(user);
                await _unitOfWork.CommitAsync();

                var result = ObjectMapper.Mapper.Map<ManagerDto>(user);
                return ApiResponse<ManagerDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<ManagerDto>.Fail(ex.Message, 500);
            }


        }

        public async Task<ApiResponse<List<ManagerDto>>> GetAllAsync()
        {
            try
            {
                var users = await _ManagerService.GetAll();

                if(users == null){
                    return ApiResponse<List<ManagerDto>>.Success(200);
                }

                var list = new List<ManagerDto>();

                foreach (var item in users)
                {
                    list.Add(ObjectMapper.Mapper.Map<ManagerDto>(item));
                }
                var result = ObjectMapper.Mapper.Map<List<ManagerDto>>(users);
                return ApiResponse<List<ManagerDto>>.Success(list,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<List<ManagerDto>>.Success(200);
            }
        }

        public async Task<ApiResponse<ManagerDto>> GetByEmailAsync(string Email)
        {
            try
            {
                if(Email == null){
                    return ApiResponse<ManagerDto>.Fail("Email field cannot be null",400);
                }
                var user = await _ManagerService.GetByEmail(Email);

                if(user == null){
                    return ApiResponse<ManagerDto>.Fail("There is no such a user",404);
                }
                var result = ObjectMapper.Mapper.Map<ManagerDto>(user);
                return ApiResponse<ManagerDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<ManagerDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<ManagerDto>> GetByIdAsync(Guid Id)
        {
            try
            {
                if(Id == Guid.Empty){
                    return ApiResponse<ManagerDto>.Fail("Guid Id cannot be null",400);
                }
                var user = await _ManagerService.GetByGuidAsync(Id);

                if(user == null){
                    return ApiResponse<ManagerDto>.Fail("There is no such a user",404);
                }
                var result = ObjectMapper.Mapper.Map<ManagerDto>(user);
                return ApiResponse<ManagerDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<ManagerDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<NoDataDto>> Remove(Guid Id)
        {
            try
            {
                if(Id == Guid.Empty){
                    return ApiResponse<NoDataDto>.Fail("Guid Id cannot be null",400);
                }
                var user = await _ManagerService.GetByGuidAsync(Id);

                if(user == null){
                    return ApiResponse<NoDataDto>.Fail("There is no such a user",404);
                }
                _ManagerService.Delete(user);
                await _unitOfWork.CommitAsync();
                return ApiResponse<NoDataDto>.Success(200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<NoDataDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<ManagerDto>> Update(UpdateManagerRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    return ApiResponse<ManagerDto>.Fail("Name section cannot be null", 400);
                }
                if (request.Surname == null)
                {
                    return ApiResponse<ManagerDto>.Fail("Surname section cannot be null", 400);
                }
                if (!request.Email.Contains("@") || !request.Email.Contains(".com"))
                {
                    return ApiResponse<ManagerDto>.Fail("Please enter a valid email ", 400);
                }
                if (request.Password == null)
                {
                    return ApiResponse<ManagerDto>.Fail("Password section cannot be null", 400);
                }
                var user = await _ManagerService.GetByGuidAsync(request.UserId);
                
                if(user == null){
                    return ApiResponse<ManagerDto>.Fail("There is no such a user",404);
                }
                user.Name = request.Name;
                user.Surname = request.Surname;
                user.Email = request.Email;
                user.Password = Convert.ToBase64String(_passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(request.Password)));
                _ManagerService.Update(user);
                await _unitOfWork.CommitAsync();

                var result = ObjectMapper.Mapper.Map<ManagerDto>(user);
                return ApiResponse<ManagerDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<ManagerDto>.Fail(ex.Message, 500);
            }
        }

        public Task<ApiResponse<IQueryable<ManagerDto>>> Where(Expression<Func<Manager, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}