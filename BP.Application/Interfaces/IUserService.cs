using CSharpFunctionalExtensions;

namespace BP.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<string, string>> Login(string email, string password);

        Task<Result<Guid, string>> Registration(string email, string password, string name);
    }
}
