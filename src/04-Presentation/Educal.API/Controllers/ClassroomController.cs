using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Contract.Requests.ClassroomRequests;
using Educal.Services.Services.ClassroomServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Educal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomController : BaseController
    {
        private readonly IClassroomService _ClassroomService;

        public ClassroomController(IClassroomService ClassroomService)
        {
            _ClassroomService = ClassroomService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClassroomRequest request){
            var result = await _ClassroomService.AddAsync(request);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id){
            var result = await _ClassroomService.GetByIdAsync(Id);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateClassroomRequest request){
            var result = await _ClassroomService.Update(request);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id){
            var result = await _ClassroomService.Remove(Id);
            return ActionResultInstance(result);
        }
    }
}