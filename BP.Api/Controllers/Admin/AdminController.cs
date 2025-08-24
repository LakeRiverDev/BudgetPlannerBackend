using BP.Api.Requests.Admin;
using BP.Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BP.Api.Controllers.Admin
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
        [Authorize]
        public async Task<IActionResult> AddUser(NewUserDto newUserDto)
        {
            var newUser = await adminService.AddUser(newUserDto.Login, newUserDto.Password, newUserDto.Email, newUserDto.Name);

            return Ok($"Add user {newUser}");
        }
    }
}
