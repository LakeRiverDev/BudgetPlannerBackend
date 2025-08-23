namespace BP.DataBase.Interfaces.Admin
{
    public interface IAdminRepository
    {
        Task<Guid> AddUser(string login, string password, string email);
    }
}
