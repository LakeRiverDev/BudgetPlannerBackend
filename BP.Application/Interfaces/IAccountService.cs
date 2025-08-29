namespace BP.Application.Interfaces
{
    public interface IAccountService
    {
        Task<decimal> GetBalance(Guid accountId);
        Task<Guid> SetLimitPerDay(Guid accountId, decimal limit);
        Task<Guid> SetLimitPerMonth(Guid accountId, decimal limit);
    }
}
