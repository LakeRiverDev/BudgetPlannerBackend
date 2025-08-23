namespace BP.Application.Interfaces.Admin
{
    public interface IAdminService
    {
        Task<Guid> AddUser(string login, string passwod, string email);
    }
}
