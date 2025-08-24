using BP.Application.Interfaces;
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

        public async Task<string> Login(string login, string password)
        {
            var searchUserByLogin = await userRepository.SearchUserByLogin(login);

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

        public Task Logout(string login)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> Registration(string login, string password, string email, string name)
        {
            var newUser = User.Create(login, password, email);
            var newOperator = Operator.Create(newUser.Id, name);
            var newAccount = Account.Create(newOperator.Id);

            var passwordHashed = passwordHasher.Hash(password);

            var registrationUser = await userRepository.Registration(newUser, newOperator, newAccount);

            return registrationUser;
        }
    }
}
