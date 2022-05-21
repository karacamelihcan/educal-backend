using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Contract.Requests.AuthenticationRequest;
using Educal.Contract.Responses;
using Educal.Core.Dtos;

namespace Educal.Services.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        
        public Task<ApiResponse<TokenDto>> CreateTokenAsync(LoginRequest login)
        {
            throw new NotImplementedException();
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