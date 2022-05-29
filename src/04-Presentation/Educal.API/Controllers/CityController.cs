using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Services.Services.CityDistServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Educal.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : BaseController
    {
        private readonly ICityDistService _service;

        public CityController(ICityDistService service)
        {
            _service = service;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCities(){
            var result = await _service.GetCities();
            return ActionResultInstance(result);
        }
  
        [HttpGet("{CityId}")]
        public async Task<IActionResult> Get(int CityId){
            var result = await _service.GetDistricts(CityId);
            return ActionResultInstance(result);
        }
    }
}