using BP.Core.Users;

namespace BP.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> Registration(string login, string password, string email, string name);
        Task<User> SearchUserByLogin(string login);
    }
}
