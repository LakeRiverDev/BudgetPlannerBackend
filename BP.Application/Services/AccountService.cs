using BP.Application.Interfaces;
using CSharpFunctionalExtensions;
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

        public async Task<Result<decimal, string>> GetBalance(Guid accountId)
        {
            var balance = await accountRepository.GetBalance(accountId);
            if (balance.IsFailure)
                return balance.Error;

            return balance;
        }

        public async Task<Result<Guid, string>> SetLimitPerDay(Guid accountId, decimal limit)
        {
            var newLimit = await accountRepository.SetLimitPerDay(accountId, limit);
            if (newLimit.IsFailure)
                return newLimit.Error;

            return newLimit;
        }

        public async Task<Result<Guid, string>> SetLimitPerMonth(Guid accountId, decimal limit)
        {
            var newLimit = await accountRepository.SetLimitPerDay(accountId, limit);
            if (newLimit.IsFailure)
                return newLimit.Error;

            return newLimit;
        }
    }
}
