namespace BP.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(string login, string password);
        Task Logout(string login);
        Task<Guid> Registration(string login, string password, string email, string name);
    }
}
