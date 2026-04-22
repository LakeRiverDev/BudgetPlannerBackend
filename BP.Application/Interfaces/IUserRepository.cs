using BP.Core.Accounts;
using BP.Core.Operators;
using BP.Core.Users;
using CSharpFunctionalExtensions;

namespace BP.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<Result<Guid, string>> Registration(User newUser, Operator newOperator, Account newAccount);
        
        Task<Result<User, string>> SearchUserByEmail(string email);

        bool UniqueEmail(string email);
    }
}
