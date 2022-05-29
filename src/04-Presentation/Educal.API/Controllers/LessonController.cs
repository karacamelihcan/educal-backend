using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Contract.Requests.LessonRequests;
using Educal.Services.Services.LessonServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Educal.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : BaseController
    {
        private readonly ILessonService _LessonService;

        public LessonController(ILessonService LessonService)
        {
            _LessonService = LessonService;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateLessonRequest request){
            var result = await _LessonService.AddAsync(request);
            return ActionResultInstance(result);
        }

   
        [HttpGet()]
        public async Task<IActionResult> GetAll(){
            var result = await _LessonService.GetLessons();
            return ActionResultInstance(result);
        }

  
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id){
            var result = await _LessonService.GetByIdAsync(Id);
            return ActionResultInstance(result);
        }

   
        [HttpPut]
        public async Task<IActionResult> Update(UpdateLessonRequest request){
            var result = await _LessonService.Update(request);
            return ActionResultInstance(result);
        }

        
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id){
            var result = await _LessonService.Remove(Id);
            return ActionResultInstance(result);
        }
    }
}