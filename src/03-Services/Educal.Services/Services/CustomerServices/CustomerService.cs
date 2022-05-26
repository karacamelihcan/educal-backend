using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Educal.Contract.Requests.CustomerRequests;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;
using Educal.Database.Repositories.CustomerRepositories;
using Educal.Database.UnitOfWorks;
using Educal.Services.MappingProfile;
using Microsoft.Extensions.Logging;

namespace Educal.Services.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomerService> _logger;
        private readonly SHA256 _passwordHasher = new SHA256CryptoServiceProvider();

        public CustomerService(ICustomerRepository customerService, IUnitOfWork unitOfWork, ILogger<CustomerService> logger)
        {
            _customerService = customerService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<CustomerDto>> AddAsync(CreateCustomerRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    return ApiResponse<CustomerDto>.Fail("Name section cannot be null", 400);
                }
                if (request.Surname == null)
                {
                    return ApiResponse<CustomerDto>.Fail("Surname section cannot be null", 400);
                }
                if (!request.Email.Contains("@") || !request.Email.Contains(".com"))
                {
                    return ApiResponse<CustomerDto>.Fail("Please enter a valid email ", 400);
                }
                if (request.Password == null)
                {
                    return ApiResponse<CustomerDto>.Fail("Password section cannot be null", 400);
                }
                var checkedUser = _customerService.GetByEmail(request.Email);
                if(checkedUser != null){
                    return ApiResponse<CustomerDto>.Fail("This email is used by a different user",400);
                }

                var user = new Customer(){
                    UserRole = Enumeration.Enums.EnumUserRole.Customer,
                    Name = request.Name,
                    Surname = request.Surname,
                    Email = request.Email,
                    Password = Convert.ToBase64String(_passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(request.Password))),
                };
                await _customerService.AddAsync(user);
                await _unitOfWork.CommitAsync();

                var result = ObjectMapper.Mapper.Map<CustomerDto>(user);
                return ApiResponse<CustomerDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<CustomerDto>.Fail(ex.Message, 500);
            }


        }

        public Task<ApiResponse<IEnumerable<CustomerDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<CustomerDto>> GetByIdAsync(Guid Id)
        {
            try
            {
                if(Id == Guid.Empty){
                    return ApiResponse<CustomerDto>.Fail("Guid Id cannot be null",400);
                }
                var user = await _customerService.GetByGuidAsync(Id);

                if(user == null){
                    return ApiResponse<CustomerDto>.Fail("There is no such a user",404);
                }
                var result = ObjectMapper.Mapper.Map<CustomerDto>(user);
                return ApiResponse<CustomerDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<CustomerDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<NoDataDto>> Remove(Guid Id)
        {
            try
            {
                if(Id == Guid.Empty){
                    return ApiResponse<NoDataDto>.Fail("Guid Id cannot be null",400);
                }
                var user = await _customerService.GetByGuidAsync(Id);

                if(user == null){
                    return ApiResponse<NoDataDto>.Fail("There is no such a user",404);
                }
                _customerService.Delete(user);
                await _unitOfWork.CommitAsync();
                return ApiResponse<NoDataDto>.Success(200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<NoDataDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<CustomerDto>> Update(UpdateCustomerRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    return ApiResponse<CustomerDto>.Fail("Name section cannot be null", 400);
                }
                if (request.Surname == null)
                {
                    return ApiResponse<CustomerDto>.Fail("Surname section cannot be null", 400);
                }
                if (!request.Email.Contains("@") || !request.Email.Contains(".com"))
                {
                    return ApiResponse<CustomerDto>.Fail("Please enter a valid email ", 400);
                }
                if (request.Password == null)
                {
                    return ApiResponse<CustomerDto>.Fail("Password section cannot be null", 400);
                }
                var user = await _customerService.GetByGuidAsync(request.UserId);
                
                if(user == null){
                    return ApiResponse<CustomerDto>.Fail("There is no such a user",404);
                }
                user.Name = request.Name;
                user.Surname = request.Surname;
                user.Email = request.Email;
                user.Password = Convert.ToBase64String(_passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(request.Password)));
                _customerService.Update(user);
                await _unitOfWork.CommitAsync();

                var result = ObjectMapper.Mapper.Map<CustomerDto>(user);
                return ApiResponse<CustomerDto>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<CustomerDto>.Fail(ex.Message, 500);
            }
        }

        public Task<ApiResponse<IQueryable<CustomerDto>>> Where(Expression<Func<Customer, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}