using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Dtos;
using Educal.Core.Models;

namespace Educal.Services.Services.TokenServices
{
    public interface ITokenService
    {
        TokenDto CreateToken(BaseUser user);
    }
}