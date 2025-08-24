using BP.Core.Users;
using BP.Infrastructure.Interfaces;
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

        public async Task<Guid> Registration(User newUser, Operator newOperator, Account newAccount)
        {
            await dbContext.Users.AddAsync(newUser);
            await dbContext.Operators.AddAsync(newOperator);
            await dbContext.Accounts.AddAsync(newAccount);

            await dbContext.SaveChangesAsync();

            return newUser.Id;
        }

        /// <summary>
        /// Поиск пользователя по почте
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<User> SearchUserByLogin(string login)
        {
            var user = dbContext.Users.Where(u => u.Login == login).FirstOrDefault();

            return user;
        }

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool UniqueEmail(string email)
        {
            var searchEmail = dbContext.Users.Where(x => x.Email == email).FirstOrDefault();

            if (searchEmail == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Найти пользователя по логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool UniqueLogin(string login)
        {
            var searchEmail = dbContext.Users.Where(x => x.Login == login).FirstOrDefault();

            if (searchEmail == null)
            {
                return true;
            }

            return false;
        }
    }
}
