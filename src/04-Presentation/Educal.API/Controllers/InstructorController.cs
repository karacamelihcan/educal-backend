using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Contract.Requests.InstructorRequests;
using Educal.Services.Services.InstructorServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Educal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : BaseController
    {
        private readonly IInstructorService _InstructorService;

        public InstructorController(IInstructorService InstructorService)
        {
            _InstructorService = InstructorService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInstructorRequest request){
            var result = await _InstructorService.AddAsync(request);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id){
            var result = await _InstructorService.GetByIdAsync(Id);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateInstructorRequest request){
            var result = await _InstructorService.Update(request);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id){
            var result = await _InstructorService.Remove(Id);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpPost("WorkingTime")]
        public async Task<IActionResult> AddWorkingTime(AddWorkingTimeRequest request){
            var result = await _InstructorService.AddWorkingTime(request);
            return ActionResultInstance(result);
        }
        [Authorize]
        [HttpPut("WorkingTime")]
        public async Task<IActionResult> UpdateWorkingTime(UpdateWorkingTimeRequest request){
            var result = await _InstructorService.UpdateWorkingTime(request);
            return ActionResultInstance(result);
        }
        [Authorize]
        [HttpDelete("WorkingTime")]
        public async Task<IActionResult> DeleteWorkingTime(DeleteWorkingTimeRequest request){
            var result = await _InstructorService.DeleteWorkingTime(request);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpGet("SearchByTime")]
        public async Task<IActionResult> GetInstructorByTimeQuery([FromQuery] GetInstructorByTimeQueryRequest request){
            var result = await _InstructorService.GetInstructorsByTimeQuery(request);
            return ActionResultInstance(result);
        }
    }
}