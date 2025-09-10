namespace BP.Application.Interfaces.Admin
{
    public interface IAdminService
    {
        Task<Guid> AddUser(string email, string passwod, string name);
    }
}
