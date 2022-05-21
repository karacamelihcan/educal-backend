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
        Task<ApiResponse<TokenDto>> CreateTokenAsync(LoginRequest login);
        Task<ApiResponse<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
        Task<ApiResponse<NoDataDto>> RevokeRefreshToken(string refreshToken);
    }
}