using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Contract.Requests.RegistrarRequests;
using Educal.Services.Services.RegistrarServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Educal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrarController : BaseController
    {
        private readonly IRegistrarService _RegistrarService;

        public RegistrarController(IRegistrarService RegistrarService)
        {
            _RegistrarService = RegistrarService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRegistrarRequest request){
            var result = await _RegistrarService.AddAsync(request);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id){
            var result = await _RegistrarService.GetByIdAsync(Id);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateRegistrarRequest request){
            var result = await _RegistrarService.Update(request);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id){
            var result = await _RegistrarService.Remove(Id);
            return ActionResultInstance(result);
        }
    }
}