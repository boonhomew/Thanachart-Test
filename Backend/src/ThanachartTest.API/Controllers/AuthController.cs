using Microsoft.AspNetCore.Mvc;
using ThanachartTest.Domain.AggregatesModel.AuthenticationAggregate;
using ThanachartTest.Domain.AggregatesModel.AuthenticationAggregate.Interfaces;
using ThanachartTest.Domain.Services;

namespace ThanachartTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (result == null)
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result)
                return BadRequest(new { message = "Username already exists" });

            return Ok(new { message = "User registered successfully" });
        }
    }
}
