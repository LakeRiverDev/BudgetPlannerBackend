using BP.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace BP.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> logger;

        public UserService(ILogger<UserService> logger)
        {
            this.logger = logger;
        }

        public Task Login(string login, string password)
        {
            throw new NotImplementedException();
        }

        public Task Logout(string login)
        {
            throw new NotImplementedException();
        }

        public Task Registration(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}
