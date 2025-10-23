using BP.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace BP.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> logger;
        private readonly IAccountRepository accountRepository;

        public AccountService(ILogger<AccountService> logger, IAccountRepository accountRepository)
        {
            this.logger = logger;
            this.accountRepository = accountRepository;
        }

        public async Task<decimal> GetBalance(Guid accountId)
        {
            var balance = await accountRepository.GetBalance(accountId);

            return balance;
        }

        public Task<Guid> SetLimitPerDay(Guid accountId, decimal limit)
        {
            var newLimit = accountRepository.SetLimitPerDay(accountId, limit);

            return newLimit;
        }

        public Task<Guid> SetLimitPerMonth(Guid accountId, decimal limit)
        {
            var newLimit = accountRepository.SetLimitPerDay(accountId, limit);

            return newLimit;
        }
    }
}
