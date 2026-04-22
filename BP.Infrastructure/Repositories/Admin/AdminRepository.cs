using BP.Application.Interfaces.Admin;
using BP.Core.Accounts;
using BP.Core.Operators;
using BP.Core.Users;
using CSharpFunctionalExtensions;
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

        public async Task<Result<Guid, string>> AddUser(string email, string password, string name)
        {
            var newUser = User.Create(null, email, password);
            if (newUser.IsFailure)
                return Result.Failure<Guid, string>(newUser.Error);
            
            logger.LogInformation("User created {@User}", newUser);

            var newOperator = Operator.Create(null, newUser.Value.Id, name);
            if (newOperator.IsFailure)
                return Result.Failure<Guid, string>(newOperator.Error);
            
            logger.LogInformation("Operator created {@Operator}", newOperator.Value);

            var newAccount = Account.Create(null, newOperator.Value.Id);
            if (newAccount.IsFailure)
                return Result.Failure<Guid, string>(newAccount.Error);
            
            logger.LogInformation("Account created {@Account}", newAccount);

            newUser.Value.AddToOperatorId(newOperator.Value.Id);
            
            await context.Users.AddAsync(newUser.Value);
            await context.Operators.AddAsync(newOperator.Value);
            await context.Accounts.AddAsync(newAccount.Value);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Result.Failure<Guid, string>(ex.Message);
            }

            return newUser.Value.Id;
        }
    }
}
