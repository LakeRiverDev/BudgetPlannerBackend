namespace BP.DataBase.Interfaces
{
    public interface IAdminRepository
    {
        Task<Guid> AddUser(string login, string password, string email);
    }
}
