using BP.Core.Users;

namespace BP.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> Registration(User newUser, Operator newOperator, Account newAccount);
        Task<User> SearchUserByLogin(string login);
    }
}
