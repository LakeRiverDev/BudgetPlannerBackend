using Microsoft.OpenApi.Models;

namespace BP.Application.Interfaces.Admin
{
    public interface IDocumentService
    {
        string GeneratePdf(OpenApiDocument swagger);
    }
}
