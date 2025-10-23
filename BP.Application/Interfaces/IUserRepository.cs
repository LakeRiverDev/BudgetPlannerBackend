using BP.Core.Accounts;
using BP.Core.Users;

namespace BP.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> Registration(User newUser, Operator newOperator, Account newAccount);
        Task<User> SearchUserByEmail(string email);        
        bool UniqueEmail(string email);
    }
}
