using Microsoft.OpenApi.Models;

namespace BP.Application.Interfaces
{
    public interface IDocumentService
    {
        string GeneratePdf(OpenApiDocument swagger);
    }
}
