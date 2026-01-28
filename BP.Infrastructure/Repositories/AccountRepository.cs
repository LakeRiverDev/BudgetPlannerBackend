using BP.Application.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BP.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий для работы с балансом
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly ILogger<AccountRepository> logger;
        private readonly BPlannerDbContext dbContext;

        public AccountRepository(ILogger<AccountRepository> logger, BPlannerDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Получить баланс
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<Result<decimal, string>> GetBalance(Guid accountId)
        {
            var account = await dbContext.Accounts
                .Where(a => a.Id == accountId)
                .FirstOrDefaultAsync();

            if (account == null)
                return Result.Failure<decimal, string>("Account not found");
            
            logger.LogInformation("Getting balance for {accountId}", accountId);
            
            return account.Balance;
        }

        /// <summary>
        /// Добавить на баланс средств
        /// </summary>
        /// <returns></returns>
        public async Task<Result<decimal, string>> AddToBalance(Guid accountId, decimal sum)
        {
            var account = await dbContext.Accounts
                .Where(a => a.Id == accountId)
                .FirstOrDefaultAsync();
            
            if (account == null)
                return Result.Failure<decimal, string>("Account not found");
            
            account.AddToBalance(sum);
            await dbContext.SaveChangesAsync();
            
            logger.LogInformation("Adding to balance for {accountId}", accountId);

            return account.Balance;
        }

        /// <summary>
        /// Убавить средства с баланса
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public async Task<Result<decimal, string>> PutToBalance(Guid accountId, decimal sum)
        {
            var account = await dbContext.Accounts
                .Where(a => a.Id == accountId)
                .FirstOrDefaultAsync();
            
            if (account == null)
                return Result.Failure<decimal, string>("Account not found");
            
            account.PutToBalance(sum);
            await dbContext.SaveChangesAsync();
            
            logger.LogInformation("Putting to balance for {accountId}", accountId);

            return account.Balance;
        }

        /// <summary>
        /// Установить лимит на день
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<Result<Guid, string>> SetLimitPerDay(Guid accountId, decimal limit)
        {
            var account = await dbContext.Accounts
                .Where(a => a.Id == accountId)
                .FirstOrDefaultAsync();
            
            if (account == null)
                return Result.Failure<Guid, string>("Account not found");
            
            account.SetLimitPerDay(limit);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Setting limit per day for {accountId}", accountId);

            return accountId;
        }

        /// <summary>
        /// Установить лимит на месяц
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<Result<Guid, string>> SetLimitPerMonth(Guid accountId, decimal limit)
        {
            var account = await dbContext.Accounts
                .Where(a => a.Id == accountId)
                .FirstOrDefaultAsync();
            
            if (account == null)
                return Result.Failure<Guid, string>("Account not found");
            
            account.SetLimitPerMonth(limit);
            await dbContext.SaveChangesAsync();
            
            logger.LogInformation("Setting limit per month for {accountId}", accountId);

            return accountId;
        }
    }
}
