namespace BP.Application.Interfaces
{
    public interface IAccountService
    {
        Task<decimal> GetBalance(Guid accountId);
    }
}
