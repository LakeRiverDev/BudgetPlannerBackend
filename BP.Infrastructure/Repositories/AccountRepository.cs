using BP.Infrastructure.Interfaces;
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
        public async Task<decimal> GetBalance(Guid accountId)
        {
            var balance = await dbContext.Accounts.Where(a => a.Id == accountId).FirstOrDefaultAsync();

            return balance.Balance;
        }

        /// <summary>
        /// Добавить на баланск средств
        /// </summary>
        /// <returns></returns>
        public async Task<decimal> AddToBalance(Guid accountId, decimal sum)
        {
            var addToBalance = await dbContext.Accounts.Where(a => a.Id == accountId).FirstOrDefaultAsync();
            addToBalance.AddToBalance(sum);

            await dbContext.SaveChangesAsync();

            return addToBalance.Balance;
        }

        /// <summary>
        /// Убавить средства с баланса
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public async Task<decimal> PutToBalance(Guid accountId, decimal sum)
        {
            var putToBalance = await dbContext.Accounts.Where(a => a.Id == accountId).FirstOrDefaultAsync();
            putToBalance.PutToBalance(sum);

            await dbContext.SaveChangesAsync();

            return putToBalance.Balance;
        }

        /// <summary>
        /// Установить лимит на день
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<Guid> SetLimitPerDay(Guid accountId, decimal limit)
        {
            var newLimit = await dbContext.Accounts.Where(a => a.Id == accountId).FirstOrDefaultAsync();
            newLimit.SetLimitPerDay(limit);

            await dbContext.SaveChangesAsync();

            return accountId;
        }

        /// <summary>
        /// Установить лимит на месяц
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<Guid> SetLimitPerMonth(Guid accountId, decimal limit)
        {
            var newLimit = await dbContext.Accounts.Where(a => a.Id == accountId).FirstOrDefaultAsync();
            newLimit.SetLimitPerMonth(limit);

            await dbContext.SaveChangesAsync();

            return accountId;
        }
    }
}
