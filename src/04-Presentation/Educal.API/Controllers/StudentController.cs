using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Contract.Requests.StudentRequests;
using Educal.Services.Services.StudentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Educal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : BaseController
    {
        private readonly IStudentService _StudentService;

        public StudentController(IStudentService StudentService)
        {
            _StudentService = StudentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentRequest request){
            var result = await _StudentService.AddAsync(request);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id){
            var result = await _StudentService.GetByIdAsync(Id);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudentRequest request){
            var result = await _StudentService.Update(request);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id){
            var result = await _StudentService.Remove(Id);
            return ActionResultInstance(result);
        }
    }
}