using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kardinal.Net.Web.Samples.Data
{
    public class SampleDbContext : DbContext
    {
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        public SampleDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WeatherConfigurations());
        }
    }
}
