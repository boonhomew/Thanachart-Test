using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThanachartTest.Domain.AggregatesModel.AuthenticationAggregate;
using ThanachartTest.Domain.AggregatesModel.AuthenticationAggregate.Interfaces;

namespace ThanachartTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuth2Controller : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public OAuth2Controller(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [AllowAnonymous]
        [HttpGet("keys")]
        [ProducesResponseType(typeof(JWKsResponse), 200)]
        public IActionResult ReadJWKs()
        {
            var result = _authenticationService.ReadJWKs();

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("verify-token")]
        [ProducesResponseType(typeof(TokenVerificationResponse), 200)]
        public IActionResult VerifyToken([FromBody] TokenVerificationRequest request)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return BadRequest(new { message = "Token is required" });
            }

            var result = _authenticationService.VerifyToken(request.Token);

            return Ok(result);
        }
    }
}
