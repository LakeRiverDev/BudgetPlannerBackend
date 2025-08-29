using BP.Core.Accounts;
using BP.Core.Operations;
using BP.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BP.Infrastructure
{
    public class BPlannerDbContext : DbContext
    {
        public BPlannerDbContext(DbContextOptions<BPlannerDbContext> options)
            : base(options) { }

        public BPlannerDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

        public DbSet<User> Users { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
