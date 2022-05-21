using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                var checkedUser = _ManagerService.GetByEmail(request.Email);
                if(checkedUser != null){
                    return ApiResponse<ManagerDto>.Fail("This email is used by a different user",400);
                }

                var user = new Manager(){
                    UserRole = Enumeration.Enums.EnumUserRole.Manager,
                    Name = request.Name,
                    Surname = request.Surname,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
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

        public Task<ApiResponse<IEnumerable<ManagerDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
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
                var checkedUser = _ManagerService.GetByEmail(request.Email);
                if(checkedUser != null){
                    return ApiResponse<ManagerDto>.Fail("This email is used by a different user",400);
                }

                var user = new Manager(){
                    UserRole = Enumeration.Enums.EnumUserRole.Manager,
                    Name = request.Name,
                    Surname = request.Surname,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                };
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