namespace BP.Application.Interfaces
{
    public interface IAdminService
    {
        Task<Guid> AddUser(string login, string passwod, string email);
    }
}
