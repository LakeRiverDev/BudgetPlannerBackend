using BP.Application.Interfaces.Admin;
using CSharpFunctionalExtensions;
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

        public async Task<Result<Guid, string>> AddUser(string email, string password, string name)
        {
            var newUserResult = await adminRepository.AddUser(email, password, name);
            if (newUserResult.IsFailure)
                return Result.Failure<Guid, string>(newUserResult.Error);
            
            logger.LogInformation($"User {email} successfully added");

            return newUserResult.Value;
        }
    }
}
