using BP.Api.Requests;
using BP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BP.Api.Controllers
{
    [Route("api/admin/users")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> logger;
        private readonly IAdminService adminService;

        public AdminController(ILogger<AdminController> logger, IAdminService adminService)
        {
            this.logger = logger;
            this.adminService = adminService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUser(NewUserDto newUserDto)
        {
            var newUser = await adminService.AddUser(newUserDto.Login, newUserDto.Password, newUserDto.Email);

            return Ok($"Add user {newUser}");
        }
    }
}
