using CSharpFunctionalExtensions;

namespace BP.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<Result<decimal, string>> GetBalance(Guid accountId);
        
        Task<Result<decimal, string>> AddToBalance(Guid accountId, decimal sum);
        
        Task<Result<decimal, string>> PutToBalance(Guid accountId, decimal sum);
        
        Task<Result<Guid, string>> SetLimitPerDay(Guid accountId, decimal limit);

        Task<Result<Guid, string>> SetLimitPerMonth(Guid accountId, decimal limit);
    }
}
