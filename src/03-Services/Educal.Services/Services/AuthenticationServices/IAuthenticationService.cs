using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Contract.Requests.AuthenticationRequest;
using Educal.Contract.Responses;
using Educal.Core.Dtos;

namespace Educal.Services.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<TokenDto>> CreateTokenAsyncCustomer(LoginRequest login);
        Task<ApiResponse<TokenDto>> CreateTokenAsyncInstructor(LoginRequest login);
        Task<ApiResponse<TokenDto>> CreateTokenAsyncManager(LoginRequest login);
        Task<ApiResponse<TokenDto>> CreateTokenAsyncRegistrar(LoginRequest login);
        Task<ApiResponse<TokenDto>> CreateTokenAsyncStudent(LoginRequest login);
        Task<ApiResponse<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
        Task<ApiResponse<NoDataDto>> RevokeRefreshToken(string refreshToken);
    }
}