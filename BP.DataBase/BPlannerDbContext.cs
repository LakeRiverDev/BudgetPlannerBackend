using BP.Core.Operations;
using BP.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BP.DataBase
{
    public class BPlannerDbContext : DbContext
    {
        public BPlannerDbContext(DbContextOptions<BPlannerDbContext> options)
            : base(options) { }

        public BPlannerDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=bplanner;User Id=postgres;Password=SkyCote36;");
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
    }
}
