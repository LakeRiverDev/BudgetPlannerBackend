using BP.Application.Interfaces;
using BP.Core.Accounts;
using BP.Core.Users;
using BP.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace BP.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> logger;
        private readonly IUserRepository userRepository;
        private readonly JwtOperations jwtOperations;
        private readonly PasswordHasher passwordHasher;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository,
            JwtOperations jwtOperations, PasswordHasher passwordHasher)
        {
            this.logger = logger;
            this.userRepository = userRepository;
            this.jwtOperations = jwtOperations;
            this.passwordHasher = passwordHasher;
        }

        public async Task<string> Login(string email, string password)
        {
            var searchUserByLogin = await userRepository.SearchUserByEmail(email);

            if (searchUserByLogin == null)
            {
                return "User is null";
            }

            var verifyPassword = passwordHasher.Verify(password, searchUserByLogin.PasswordHash);

            if (verifyPassword == false)
            {
                return "Incorrect password";
            }

            var token = jwtOperations.Generate(searchUserByLogin);

            return token;
        }

        public async Task<Guid> Registration(string email, string password, string name)
        {
            var newUser = User.Create(email, password);
            var newOperator = Operator.Create(newUser.Id, name);
            var newAccount = Account.Create(newOperator.Id);

            var passwordHashed = passwordHasher.Hash(password);

            var registrationUser = await userRepository.Registration(newUser, newOperator, newAccount);

            return registrationUser;
        }
    }
}
