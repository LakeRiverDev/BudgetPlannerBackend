using BP.DataBase.Interfaces;
using BP.DataBase.Models;
using Microsoft.Extensions.Logging;

namespace BP.DataBase.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ILogger<AdminRepository> logger;
        private readonly BPlannerDbContext context;

        public AdminRepository(ILogger<AdminRepository> logger, BPlannerDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        public async Task<Guid> AddOperator(Guid guid)
        {
            var newOperator = new Operator
            {
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                Id = Guid.NewGuid(),
                UserId = guid
            };

            await context.Operators.AddAsync(newOperator);

            return newOperator.Id;
        }

        public async Task<Guid> AddUser(string login, string password, string email)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Login = login,
                PasswordHash = password,
                Name = login + "_name",
                Email = email,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            var newOperator = await AddOperator(newUser.Id);
            newUser.OperatorId = newOperator;

            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();

            return newUser.Id;
        }
    }
}
