using BP.Application.Interfaces;
using BP.Core.Accounts;
using BP.Core.Operators;
using BP.Core.Users;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace BP.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> logger;
        private readonly IUserRepository userRepository;
        private readonly IJwtOperations jwtOperations;
        private readonly IPasswordHasher passwordHasher;

        public UserService(
            ILogger<UserService> logger,
            IUserRepository userRepository,
            IJwtOperations jwtOperations, IPasswordHasher passwordHasher)
        {
            this.logger = logger;
            this.userRepository = userRepository;
            this.jwtOperations = jwtOperations;
            this.passwordHasher = passwordHasher;
        }

        public async Task<Result<string, string>> Login(string email, string password)
        {
            var searchUserByLogin = await userRepository.SearchUserByEmail(email);

            if (searchUserByLogin.IsFailure)
                return Result.Failure<string, string>("User is null");

            var verifyPassword = passwordHasher.Verify(password, searchUserByLogin.Value.PasswordHash);

            if (verifyPassword == false)
                return Result.Failure<string, string>("Incorrect password");

            var token = jwtOperations.Generate(searchUserByLogin.Value);

            logger.LogInformation("User {email} successfully logged in", email);

            return Result.Success<string, string>(token);
        }

        public async Task<Result<Guid, string>> Registration(string email, string password, string name)
        {
            var passwordHashed = passwordHasher.Hash(password);

            var existingUser = await userRepository.SearchUserByEmail(email);
            if (existingUser.IsSuccess)
                return Result.Failure<Guid, string>("User with this email already exists");
            
            var newUser = User.Create(null, email, passwordHashed);
            var newOperator = Operator.Create(null, newUser.Value.Id, name);
            var newAccount = Account.Create(null, newOperator.Value.Id);
            
            newUser.Value.AddToOperatorId(newOperator.Value.Id);
            newOperator.Value.AddToAccountId(newAccount.Value.Id);

            var registrationUser = await userRepository.Registration(
                newUser.Value,
                newOperator.Value,
                newAccount.Value);

            logger.LogInformation("User {email} successfully registered", email);

            return Result.Success<Guid, string>(registrationUser.Value);
        }
    }
}