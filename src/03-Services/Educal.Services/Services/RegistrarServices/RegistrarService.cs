using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Educal.Contract.Requests.RegistrarRequests;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;
using Educal.Database.Repositories.RegistrarRepositories;
using Educal.Database.UnitOfWorks;
using Educal.Services.MappingProfile;
using Microsoft.Extensions.Logging;

namespace Educal.Services.Services.RegistrarServices
{
    public class RegistrarService : IRegistrarService
    {
        private readonly IRegistrarRepository _RegistrarService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RegistrarService> _logger;
        private readonly SHA256 _passwordHasher = new SHA256CryptoServiceProvider();

        public RegistrarService(IRegistrarRepository RegistrarService, IUnitOfWork unitOfWork, ILogger<RegistrarService> logger)
        {
            _RegistrarService = RegistrarService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<RegistrarDto>> AddAsync(CreateRegistrarRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    return ApiResponse<RegistrarDto>.Fail("Name section cannot be null", 400);
                }
                if (request.Surname == null)
                {
                    return ApiResponse<RegistrarDto>.Fail("Surname section cannot be null", 400);
                }
                if (!request.Email.Contains("@") || !request.Email.Contains(".com"))
                {
                    return ApiResponse<RegistrarDto>.Fail("Please enter a valid email ", 400);
                }
                if (request.Password == null)
                {
                    return ApiResponse<RegistrarDto>.Fail("Password section cannot be null", 400);
                }
                var checkedUser = _RegistrarService.GetByEmail(request.Email);
                if(checkedUser != null){
                    return ApiResponse<RegistrarDto>.Fail("This email is used by a different user",400);
                }

                var user = new Registrar(){
                    UserRole = Enumeration.Enums.EnumUserRole.Registrar,
                    Name = request.Name,
                    Surname = request.Surname,
                    Email = request.Email,
                    Password = Convert.ToBase64String(_passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(request.Password))),
                };
                await _RegistrarService.AddAsync(user);
                await _unitOfWork.CommitAsync();

                var result = ObjectMapper.Mapper.Map<RegistrarDto>(user);
                return ApiResponse<RegistrarDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<RegistrarDto>.Fail(ex.Message, 500);
            }


        }

        public Task<ApiResponse<IEnumerable<RegistrarDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<RegistrarDto>> GetByIdAsync(Guid Id)
        {
            try
            {
                if(Id == Guid.Empty){
                    return ApiResponse<RegistrarDto>.Fail("Guid Id cannot be null",400);
                }
                var user = await _RegistrarService.GetByGuidAsync(Id);

                if(user == null){
                    return ApiResponse<RegistrarDto>.Fail("There is no such a user",404);
                }
                var result = ObjectMapper.Mapper.Map<RegistrarDto>(user);
                return ApiResponse<RegistrarDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<RegistrarDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<NoDataDto>> Remove(Guid Id)
        {
            try
            {
                if(Id == Guid.Empty){
                    return ApiResponse<NoDataDto>.Fail("Guid Id cannot be null",400);
                }
                var user = await _RegistrarService.GetByGuidAsync(Id);

                if(user == null){
                    return ApiResponse<NoDataDto>.Fail("There is no such a user",404);
                }
                _RegistrarService.Delete(user);
                await _unitOfWork.CommitAsync();
                return ApiResponse<NoDataDto>.Success(200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<NoDataDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<RegistrarDto>> Update(UpdateRegistrarRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    return ApiResponse<RegistrarDto>.Fail("Name section cannot be null", 400);
                }
                if (request.Surname == null)
                {
                    return ApiResponse<RegistrarDto>.Fail("Surname section cannot be null", 400);
                }
                if (!request.Email.Contains("@") || !request.Email.Contains(".com"))
                {
                    return ApiResponse<RegistrarDto>.Fail("Please enter a valid email ", 400);
                }
                if (request.Password == null)
                {
                    return ApiResponse<RegistrarDto>.Fail("Password section cannot be null", 400);
                }
                var user = await _RegistrarService.GetByGuidAsync(request.UserId);
                
                if(user == null){
                    return ApiResponse<RegistrarDto>.Fail("There is no such a user",404);
                }
                user.Name = request.Name;
                user.Surname = request.Surname;
                user.Email = request.Email;
                user.Password = Convert.ToBase64String(_passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(request.Password)));
                _RegistrarService.Update(user);
                await _unitOfWork.CommitAsync();

                var result = ObjectMapper.Mapper.Map<RegistrarDto>(user);
                return ApiResponse<RegistrarDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<RegistrarDto>.Fail(ex.Message, 500);
            }
        }

        public Task<ApiResponse<IQueryable<RegistrarDto>>> Where(Expression<Func<Registrar, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}