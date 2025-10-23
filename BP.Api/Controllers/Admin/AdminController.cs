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

        [HttpGet("get-pdf-document")]
        public async Task<IActionResult> GetPdfDocumentation()
        {
            var swagger = swaggerProvider.GetSwagger("v1");

            // Генерируем HTML           
            var htmlContent = documentService.GeneratePdf(swagger);

            // Конвертируем в PDF
            var converter = new BasicConverter(new PdfTools());
            var document = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        HtmlContent = htmlContent,
                        WebSettings =
                        {
                            DefaultEncoding = "utf-8"
                        }
                    }
                }
            };

            var pdfBytes = converter.Convert(document);
            return File(pdfBytes, "application/pdf", "api-documentation.pdf");
        }
    }
}
