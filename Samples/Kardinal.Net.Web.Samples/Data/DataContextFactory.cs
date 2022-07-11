using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace Kardinal.Net.Web.Samples.Data
{
    /// <summary>
    /// Classe de fábrica de contexto.
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<SampleDbContext>
    {
        /// <summary>
        /// Método que efetuará a criação do contexto.
        /// </summary>
        /// <param name="args">argumentos de criação.</param>
        /// <returns>Contexto criado.</returns>
        public SampleDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SampleDbContext>();
            var migrationsAssembly = typeof(SampleDbContext).GetTypeInfo().Assembly.GetName().Name;
            builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Kardinal;Integrated Security=true;");

            var context = new SampleDbContext(builder.Options);
            return context;
        }
    }
}
