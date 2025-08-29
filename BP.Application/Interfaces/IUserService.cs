namespace BP.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(string login, string password);        
        Task<Guid> Registration(string login, string password, string email, string name);
    }
}
