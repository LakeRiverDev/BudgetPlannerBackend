using BP.Core.Users;
using BP.Infrastructure.Interfaces.Admin;
using Microsoft.Extensions.Logging;

namespace BP.Infrastructure.Repositories.Admin
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ILogger<AdminRepository> logger;
        private readonly BPlannerDbContext context;

        public AdminRepository(ILogger<AdminRepository> logger, BPlannerDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<Guid> AddUser(string login, string password, string email, string name)
        {
            var newUser = User.Create(login, password, email);
            var newOperator = Operator.Create(newUser.Id, name);
            var newAccount = Account.Create(newOperator.Id);

            newUser.OperatorId = newOperator.Id;

            await context.Users.AddAsync(newUser);
            await context.Operators.AddAsync(newOperator);
            await context.Accounts.AddAsync(newAccount);

            await context.SaveChangesAsync();

            return newUser.Id;
        }
    }
}
