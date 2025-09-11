namespace BP.Infrastructure.Interfaces.Admin
{
    public interface IAdminRepository
    {
        Task<Guid> AddUser(string email, string password, string name);
    }
}
