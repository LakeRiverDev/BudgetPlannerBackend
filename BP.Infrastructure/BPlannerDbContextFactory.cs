using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BP.Infrastructure
{
    public class BPlannerDbContextFactory : IDesignTimeDbContextFactory<BPlannerDbContext>
    {
        public BPlannerDbContext CreateDbContext(string[] args)
        {
            //var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
            var connectionString = "Server=localhost;Port=5432;Database=bplanner;User Id=postgres;Password=SkyCote36";

            Console.WriteLine(connectionString);

            if (string.IsNullOrEmpty(connectionString))
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())                    
                    .AddJsonFile("appsettings.json")
                    .Build();

                connectionString = configuration.GetConnectionString("ConnectionString");
            }           

            var optionsBuilder = new DbContextOptionsBuilder<BPlannerDbContext>();
            optionsBuilder.UseNpgsql(connectionString);            

            return new BPlannerDbContext(optionsBuilder.Options);
        }
    }
}
