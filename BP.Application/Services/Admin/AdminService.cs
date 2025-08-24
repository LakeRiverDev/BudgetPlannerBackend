using BP.Application.Interfaces.Admin;
using BP.Infrastructure.Interfaces.Admin;
using Microsoft.Extensions.Logging;

namespace BP.Application.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly ILogger<AdminService> logger;
        private readonly IAdminRepository adminRepository;

        public AdminService(ILogger<AdminService> logger, IAdminRepository adminRepository)
        {
            this.logger = logger;
            this.adminRepository = adminRepository;
        }

        public async Task<Guid> AddUser(string login, string password, string email, string name)
        {
            var newUserId = await adminRepository.AddUser(login, password, email, name);

            return newUserId;
        }
    }
}
