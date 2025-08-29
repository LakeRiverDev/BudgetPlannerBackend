using BP.Api.Requests;
using BP.Application.Interfaces;
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
            var register = await userService.Registration(registrationDto.Login, registrationDto.Password, registrationDto.Email, registrationDto.Name);

            return Ok("User registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var ip = HttpContext.Request.Headers["Host"];
            var device = HttpContext.Request.Headers["User-Agent"];

            var login = await userService.Login(loginDto.Login, loginDto.Password);

            var httpContext = HttpContext;
            httpContext.Response.Cookies.Append("bp", login);            

            return Ok("User logged in");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var httpContext = HttpContext;
            httpContext.Response.Cookies.Delete("bp");

            return Ok("User logged out");
        }
    }
}
