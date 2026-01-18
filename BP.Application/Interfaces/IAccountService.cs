using CSharpFunctionalExtensions;

namespace BP.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Result<decimal, string>> GetBalance(Guid accountId);

        Task<Result<Guid, string>> SetLimitPerDay(Guid accountId, decimal limit);

        Task<Result<Guid, string>> SetLimitPerMonth(Guid accountId, decimal limit);
    }
}
