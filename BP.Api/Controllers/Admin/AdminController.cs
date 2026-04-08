using BP.Application.Interfaces.Admin;
using BP.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;

namespace BP.Api.Controllers.Admin
{
    [Route("api/admin/")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> logger;
        private readonly IAdminService adminService;
        private readonly IDocumentService documentService;
        private readonly ISwaggerProvider swaggerProvider;

        public AdminController(ILogger<AdminController> logger, IAdminService adminService, IDocumentService documentService, ISwaggerProvider swaggerProvider)
        {
            this.logger = logger;
            this.adminService = adminService;
            this.documentService = documentService;
            this.swaggerProvider = swaggerProvider;
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser(NewUserDto newUserDto)
        {
            var newUser = await adminService.AddUser(newUserDto.Email, newUserDto.Password, newUserDto.Name);

            return Ok($"Add user {newUser}");
        }
    }
}
