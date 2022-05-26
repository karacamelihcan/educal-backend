using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Contract.Requests.CustomerRequests;
using Educal.Services.Services.CustomerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Educal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerRequest request){
            var result = await _customerService.AddAsync(request);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id){
            var result = await _customerService.GetByIdAsync(Id);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerRequest request){
            var result = await _customerService.Update(request);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id){
            var result = await _customerService.Remove(Id);
            return ActionResultInstance(result);
        }
    }
}