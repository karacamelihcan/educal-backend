using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Educal.Core.Dtos;
using Educal.Core.Models;
using Educal.Database.Repositories.CustomerRepositories;
using Educal.Database.Repositories.InstructorRepositories;
using Educal.Database.Repositories.ManagerRepositories;
using Educal.Database.Repositories.RegistrarRepositories;
using Educal.Database.Repositories.StudentRepositories;
using Educal.Services.Services.SignServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Educal.Services.Services.TokenServices
{
    public class TokenService : ITokenService
    {
        private readonly TokenOptions _tokenOptions;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }

        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> GetClaims(BaseUser user)
        {
            var userInfo = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, user.Guid.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.Name , user.Name + " "+ user.Surname),
                new Claim(ClaimTypes.Role,user.UserRole.ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,DateTime.UtcNow.AddHours(_tokenOptions.AccessTokenExpiration).ToString())
            };
            return userInfo;
        }
        public TokenDto CreateToken(BaseUser user)
        {
            try
            {
                var accessTokenExpiration = DateTime.UtcNow.AddHours(_tokenOptions.AccessTokenExpiration);
                var refreshTokenExpiration = DateTime.UtcNow.AddHours(_tokenOptions.RefreshTokenExpiration);
                var securityKey = SignService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);
                SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: _tokenOptions.Issuer,
                    expires: accessTokenExpiration,
                    notBefore: DateTime.UtcNow,
                    claims: GetClaims(user),
                    signingCredentials: signingCredentials
                );
                var handler = new JwtSecurityTokenHandler();
                var token = handler.WriteToken(jwtSecurityToken);
                var TokenDto = new TokenDto()
                {
                    AccessToken = token,
                    RefreshToken = CreateRefreshToken(),
                    AccessTokenExpiration = accessTokenExpiration,
                    RefreshTokenExpiration = refreshTokenExpiration
                };
                return TokenDto;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message); 
                return new TokenDto();               
            }
        }
    }
}