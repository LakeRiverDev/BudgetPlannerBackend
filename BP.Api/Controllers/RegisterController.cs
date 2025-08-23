using BP.Api.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BP.Api.Controllers
{
    [Route("api/v1/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> logger;

        public RegisterController(ILogger<RegisterController> logger)
        {
            this.logger = logger;
        }

        [HttpPost("register")]
        public void Register(RegistrationDto registrationDto) { }

        [HttpPost("login")]
        public void Login() { }

        [HttpPost("logout")]
        public void Logout() { }
    }
}
