using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Contract.Requests.AuthenticationRequest;
using Educal.Services.Services.AuthenticationServices;
using Microsoft.AspNetCore.Mvc;

namespace Educal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("Customer")]
        public async Task<IActionResult> Customer(LoginRequest request){
            var result = await _authService.CreateTokenAsyncCustomer(request);
            return ActionResultInstance(result);
        }

        [HttpPost("Instructor")]
        public async Task<IActionResult> Instructor(LoginRequest request){
            var result = await _authService.CreateTokenAsyncInstructor(request);
            return ActionResultInstance(result);
        }

        [HttpPost("Manager")]
        public async Task<IActionResult> Manager(LoginRequest request){
            var result = await _authService.CreateTokenAsyncManager(request);
            return ActionResultInstance(result);
        }
        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar(LoginRequest request){
            var result = await _authService.CreateTokenAsyncRegistrar(request);
            return ActionResultInstance(result);
        }
        [HttpPost("Student")]
        public async Task<IActionResult> Student(LoginRequest request){
            var result = await _authService.CreateTokenAsyncStudent(request);
            return ActionResultInstance(result);
        }
    }
}