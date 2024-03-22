using InternshipManagementSystem.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InternshipManagementSystem.Persistence
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<InternshipManagementSystemDbContext>
    {
        public InternshipManagementSystemDbContext CreateDbContext(string[] args)
        {
            //TODO: GetCurrentDirectory could throw exception in cloud
         ConfigurationManager configurationManager = new();
         configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/InternshipManagementSystem.API"));
         configurationManager.AddJsonFile("appsettings.json");

         DbContextOptionsBuilder<InternshipManagementSystemDbContext> dbContextOptionsBuilder = new();

         dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);

         //dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);


            return new(dbContextOptionsBuilder.Options);
        }
    }
}
