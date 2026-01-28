using CSharpFunctionalExtensions;

namespace BP.Application.Interfaces.Admin
{
    public interface IAdminRepository
    {
        Task<Result<Guid, string>> AddUser(string email, string password, string name);
    }
}
