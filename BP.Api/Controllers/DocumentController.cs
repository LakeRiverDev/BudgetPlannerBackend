using BP.Application.Interfaces;
using DinkToPdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

namespace BP.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly ISwaggerProvider swaggerProvider;
        private readonly IDocumentService documentService;

        public DocumentController(ISwaggerProvider swaggerProvider, IDocumentService documentService)
        {
            this.swaggerProvider = swaggerProvider;
            this.documentService = documentService;
        }

        [HttpGet("pdf")]
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
