using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Contract.Requests.ManagerRequests;
using Educal.Services.Services.ManagerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Educal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagerController : BaseController
    {
        private readonly IManagerService _ManagerService;

        public ManagerController(IManagerService ManagerService)
        {
            _ManagerService = ManagerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateManagerRequest request){
            var result = await _ManagerService.AddAsync(request);
            return ActionResultInstance(result);
        }

        
        [HttpGet("{Id}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid Id){
            var result = await _ManagerService.GetByIdAsync(Id);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateManagerRequest request){
            var result = await _ManagerService.Update(request);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id){
            var result = await _ManagerService.Remove(Id);
            return ActionResultInstance(result);
        }
        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetAll(){
            var result = await _ManagerService.GetAllAsync();
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpGet("Email/{Email}")]
        public async Task<IActionResult> GetByEmail(string Email){
            var result = await _ManagerService.GetByEmailAsync(Email);
            return ActionResultInstance(result);
        }
    }
}