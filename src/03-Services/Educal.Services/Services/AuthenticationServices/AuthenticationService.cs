using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Contract.Requests.AuthenticationRequest;
using Educal.Contract.Responses;
using Educal.Core.Dtos;
using Educal.Core.Models;
using Educal.Database.Repositories.CustomerRepositories;
using Educal.Database.Repositories.InstructorRepositories;
using Educal.Database.Repositories.ManagerRepositories;
using Educal.Database.Repositories.RegistrarRepositories;
using Educal.Database.Repositories.StudentRepositories;
using Educal.Database.Repositories.TokenRepositories;
using Educal.Database.UnitOfWorks;
using Educal.Services.Services.TokenServices;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Educal.Services.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private ILogger<AuthenticationService> _logger;
        private readonly ICustomerRepository _customerRepo;
        private readonly IInstructorRepository _instructorRepo;
        private readonly IManagerRepository _managerRepo;
        private readonly IRegistrarRepository _registrarRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly ITokenRepository _tokenRepo;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SHA256 _passwordHasher = new SHA256CryptoServiceProvider();

        public AuthenticationService(ICustomerRepository customerRepo, IInstructorRepository instructorRepo, IManagerRepository managerRepo, IRegistrarRepository registrarRepo, IStudentRepository studentRepo, ITokenRepository tokenRepo, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _customerRepo = customerRepo;
            _instructorRepo = instructorRepo;
            _managerRepo = managerRepo;
            _registrarRepo = registrarRepo;
            _studentRepo = studentRepo;
            _tokenRepo = tokenRepo;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<TokenDto>> CreateTokenAsyncCustomer(LoginRequest login)
        {
            try
            {
                if (login.Email == null || login.Password == null)
                {
                    return ApiResponse<TokenDto>.Fail("Email and password cannot be null", 400);
                }
                var user = await _customerRepo.GetByEmail(login.Email);
                if (user == null)
                {
                    return ApiResponse<TokenDto>.Fail("Email or password is wrong", 400);
                }
                var hashResult = Convert.ToBase64String(_passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(login.Password)));
                if (hashResult != user.Password)
                {
                    return ApiResponse<TokenDto>.Fail("Email or password is wrong", 400);
                }
                var token = _tokenService.CreateToken(user);
                var userRefreshToken = await _tokenRepo.Where(x => x.UserId == user.Guid);
                var RefreshToken = await userRefreshToken.SingleOrDefaultAsync();
                if (RefreshToken == null)
                {
                    await _tokenRepo.AddAsync(new UserRefreshToken
                    {
                        UserId = user.Guid,
                        Token = token.RefreshToken,
                        Expiration = token.RefreshTokenExpiration
                    });
                }
                else
                {
                    RefreshToken.Token = token.RefreshToken;
                    RefreshToken.Expiration = token.RefreshTokenExpiration;
                }
                await _unitOfWork.CommitAsync();
                return ApiResponse<TokenDto>.Success(token, 200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<TokenDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<TokenDto>> CreateTokenAsyncInstructor(LoginRequest login)
        {
            try
            {
                if (login.Email == null || login.Password == null)
                {
                    return ApiResponse<TokenDto>.Fail("Email and password cannot be null", 400);
                }
                var user = await _instructorRepo.GetByEmail(login.Email);
                if (user == null)
                {
                    return ApiResponse<TokenDto>.Fail("Email or password is wrong", 400);
                }
                var hashResult = Convert.ToBase64String(_passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(login.Password)));
                if (hashResult != user.Password)
                {
                    return ApiResponse<TokenDto>.Fail("Email or password is wrong", 400);
                }
                var token = _tokenService.CreateToken(user);
                var userRefreshToken = await _tokenRepo.Where(x => x.UserId == user.Guid);
                var RefreshToken = await userRefreshToken.SingleOrDefaultAsync();
                if (RefreshToken == null)
                {
                    await _tokenRepo.AddAsync(new UserRefreshToken
                    {
                        UserId = user.Guid,
                        Token = token.RefreshToken,
                        Expiration = token.RefreshTokenExpiration
                    });
                }
                else
                {
                    RefreshToken.Token = token.RefreshToken;
                    RefreshToken.Expiration = token.RefreshTokenExpiration;
                }
                await _unitOfWork.CommitAsync();
                return ApiResponse<TokenDto>.Success(token, 200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<TokenDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<TokenDto>> CreateTokenAsyncManager(LoginRequest login)
        {
            try
            {
                if (login.Email == null || login.Password == null)
                {
                    return ApiResponse<TokenDto>.Fail("Email and password cannot be null", 400);
                }
                var user = await _managerRepo.GetByEmail(login.Email);
                if (user == null)
                {
                    return ApiResponse<TokenDto>.Fail("Email or password is wrong", 400);
                }
                var hashResult = Convert.ToBase64String(_passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(login.Password)));
                if (hashResult != user.Password)
                {
                    return ApiResponse<TokenDto>.Fail("Email or password is wrong", 400);
                }
                var token = _tokenService.CreateToken(user);
                var userRefreshToken = await _tokenRepo.Where(x => x.UserId == user.Guid);
                var RefreshToken = await userRefreshToken.SingleOrDefaultAsync();
                if (RefreshToken == null)
                {
                    await _tokenRepo.AddAsync(new UserRefreshToken
                    {
                        UserId = user.Guid,
                        Token = token.RefreshToken,
                        Expiration = token.RefreshTokenExpiration
                    });
                }
                else
                {
                    RefreshToken.Token = token.RefreshToken;
                    RefreshToken.Expiration = token.RefreshTokenExpiration;
                }
                await _unitOfWork.CommitAsync();
                return ApiResponse<TokenDto>.Success(token, 200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<TokenDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<TokenDto>> CreateTokenAsyncRegistrar(LoginRequest login)
        {
            try
            {
                if (login.Email == null || login.Password == null)
                {
                    return ApiResponse<TokenDto>.Fail("Email and password cannot be null", 400);
                }
                var user = await _registrarRepo.GetByEmail(login.Email);
                if (user == null)
                {
                    return ApiResponse<TokenDto>.Fail("Email or password is wrong", 400);
                }
                var hashResult = Convert.ToBase64String(_passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(login.Password)));
                if (hashResult != user.Password)
                {
                    return ApiResponse<TokenDto>.Fail("Email or password is wrong", 400);
                }
                var token = _tokenService.CreateToken(user);
                var userRefreshToken = await _tokenRepo.Where(x => x.UserId == user.Guid);
                var RefreshToken = await userRefreshToken.SingleOrDefaultAsync();
                if (RefreshToken == null)
                {
                    await _tokenRepo.AddAsync(new UserRefreshToken
                    {
                        UserId = user.Guid,
                        Token = token.RefreshToken,
                        Expiration = token.RefreshTokenExpiration
                    });
                }
                else
                {
                    RefreshToken.Token = token.RefreshToken;
                    RefreshToken.Expiration = token.RefreshTokenExpiration;
                }
                await _unitOfWork.CommitAsync();
                return ApiResponse<TokenDto>.Success(token, 200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<TokenDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ApiResponse<TokenDto>> CreateTokenAsyncStudent(LoginRequest login)
        {
            try
            {
                if (login.Email == null || login.Password == null)
                {
                    return ApiResponse<TokenDto>.Fail("Email and password cannot be null", 400);
                }
                var user = await _studentRepo.GetByEmail(login.Email);
                if (user == null)
                {
                    return ApiResponse<TokenDto>.Fail("Email or password is wrong", 400);
                }
                var hashResult = Convert.ToBase64String(_passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(login.Password)));
                if (hashResult != user.Password)
                {
                    return ApiResponse<TokenDto>.Fail("Email or password is wrong", 400);
                }
                var token = _tokenService.CreateToken(user);
                var userRefreshToken = await _tokenRepo.Where(x => x.UserId == user.Guid);
                var RefreshToken = await userRefreshToken.SingleOrDefaultAsync();
                if (RefreshToken == null)
                {
                    await _tokenRepo.AddAsync(new UserRefreshToken
                    {
                        UserId = user.Guid,
                        Token = token.RefreshToken,
                        Expiration = token.RefreshTokenExpiration
                    });
                }
                else
                {
                    RefreshToken.Token = token.RefreshToken;
                    RefreshToken.Expiration = token.RefreshTokenExpiration;
                }
                await _unitOfWork.CommitAsync();
                return ApiResponse<TokenDto>.Success(token, 200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<TokenDto>.Fail(ex.Message, 500);
            }
        }

        public Task<ApiResponse<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}