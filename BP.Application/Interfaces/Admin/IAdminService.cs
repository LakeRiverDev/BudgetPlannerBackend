using CSharpFunctionalExtensions;

namespace BP.Application.Interfaces.Admin
{
    public interface IAdminService
    {
        Task<Result<Guid, string>> AddUser(string email, string password, string name);
    }
}
