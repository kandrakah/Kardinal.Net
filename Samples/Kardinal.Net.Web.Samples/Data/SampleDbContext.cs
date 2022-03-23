using Kardinal.Net.Web.Samples.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace Kardinal.Net.Web.Samples.Data
{
    public class SampleDbContext : DbContext
    {
        private readonly IDataProtectionProvider _provider;

        public SampleDbContext(DbContextOptions options, IDataProtectionProvider provider) : base(options)
        {
            this._provider = provider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WeatherConfigurations(this._provider));
        }
    }
}
