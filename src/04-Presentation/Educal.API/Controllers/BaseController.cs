using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Contract.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Educal.API.Controllers
{
    public class BaseController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult ActionResultInstance<T>(ApiResponse<T> response) where T: class
        {
            return new ObjectResult(response){
                StatusCode = response.StatusCode
            };
        }
    }
}