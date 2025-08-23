namespace BP.Application.Interfaces
{
    public interface IUserService
    {
        Task Login(string login, string password);

        Task Logout(string login);

        Task Registration(string login, string password);
    }
}
