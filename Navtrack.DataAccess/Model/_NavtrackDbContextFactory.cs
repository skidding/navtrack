using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Navtrack.DataAccess.Repository;
using Navtrack.Library.DI;

namespace Navtrack.DataAccess.Model
{
    [Service(typeof(IDbContextFactory))]
    // ReSharper disable once InconsistentNaming
    public class _NavtrackDbContextFactory : IDbContextFactory
    {
        private readonly IConfiguration configuration;

        public _NavtrackDbContextFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbContext CreateDbContext()
        {
            DbContextOptionsBuilder<NavtrackContext> optionsBuilder =
                new DbContextOptionsBuilder<NavtrackContext>();

            string connectionString = configuration.GetConnectionString("navtrack");
            
            optionsBuilder.UseSqlServer(connectionString);

            return new NavtrackContext(optionsBuilder.Options);
        }
    }
}