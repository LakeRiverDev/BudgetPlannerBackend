using BP.Core.Users;
using BP.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace BP.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> logger;
        private readonly BPlannerDbContext context;

        public UserRepository(ILogger<UserRepository> logger, BPlannerDbContext dbContext)
        {
            this.logger = logger;
            this.context = dbContext;
        }

        public async Task<Guid> Registration(User newUser, Operator newOperator, Account newAccount)
        {            
            await context.Users.AddAsync(newUser);
            await context.Operators.AddAsync(newOperator);
            await context.Accounts.AddAsync(newAccount);

            await context.SaveChangesAsync();

            return newUser.Id;
        }

        public async Task<User> SearchUserByLogin(string login)
        {
            var user = context.Users.Where(u => u.Login == login).FirstOrDefault();

            return user;
        }
    }
}
