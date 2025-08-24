namespace BP.Infrastructure.Interfaces
{
    public interface IAccountRepository
    {
        Task<decimal> GetBalance(Guid accountId);
        Task<decimal> AddToBalance(Guid accountId, decimal sum);
        Task<decimal> PutToBalance(Guid accountId, decimal sum);
    }
}
