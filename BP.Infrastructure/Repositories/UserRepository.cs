using BP.Application.Interfaces;
using BP.Core.Accounts;
using BP.Core.Operators;
using BP.Core.Users;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BP.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> logger;
        private readonly BPlannerDbContext dbContext;

        public UserRepository(ILogger<UserRepository> logger, BPlannerDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<Result<Guid, string>> Registration(User newUser, Operator newOperator, Account newAccount)
        {
            await dbContext.Users.AddAsync(newUser);
            await dbContext.Operators.AddAsync(newOperator);
            await dbContext.Accounts.AddAsync(newAccount);
            
            try
            {
                await dbContext.SaveChangesAsync();
                
                logger.LogInformation("User created a new account with password");
            }
            catch (Exception ex)
            {
                return Result.Failure<Guid, string>(ex.Message);
            }
            
            return newUser.Id;
        }

        /// <summary>
        /// Поиск пользователя по почте
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<Result<User, string>> SearchUserByEmail(string email)
        {
            var user = await dbContext.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            if (user == null)
                return Result.Failure<User, string>("User not found");

            return user;
        }

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool UniqueEmail(string email)
        {
            var searchEmail = dbContext.Users
                .Where(x => x.Email == email)
                .FirstOrDefault();

            if (searchEmail == null)
                return true;

            return false;
        }        
    }
}
