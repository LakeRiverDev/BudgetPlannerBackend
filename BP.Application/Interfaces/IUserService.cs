namespace BP.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(string email, string password);        
        Task<Guid> Registration(string email, string password, string name);
    }
}
