using BP.Application.Interfaces;
using BP.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BP.Api.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IUserService userService;

        public AuthController(ILogger<AuthController> logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationDto registrationDto)
        {
            var register = await userService.Registration(
                registrationDto.Email, 
                registrationDto.Password, 
                registrationDto.Name);
            
            if (register.IsFailure)
                return BadRequest(register.Error);

            return Ok($"User registered {register.Value}");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var login = await userService.Login(loginDto.Email, loginDto.Password);
            if (login.IsFailure)
                return BadRequest(login.Error);

            var httpContext = HttpContext;
            httpContext.Response.Cookies.Append("bp", login.Value);

            return Ok($"User logged in {login.Value}");
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var httpContext = HttpContext;
            httpContext.Response.Cookies.Delete("bp");

            return Ok("User logged out");
        }
    }
}
