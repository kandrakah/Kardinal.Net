/*
Kardinal.Net
Copyright (C) 2022 Marcelo O. Mendes

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with this program; if not, write to the Free Software Foundation,
Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using Kardinal.Net.Web.Data.EntityFramework.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de extensões para <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Extensão que aplica a migração à um contexto.
        /// </summary>
        /// <typeparam name="TContext">Contexto o qual terá a migração aplicada.</typeparam>
        /// <param name="builder">Objeto referenciado.</param>
        public static void ApplyMigrations<TContext>(this IApplicationBuilder builder) where TContext : DbContext
        {
            builder.ApplyMigrationsAsync<TContext>().Wait();
        }

        /// <summary>
        /// Extensão que aplica a migração à um contexto.
        /// </summary>
        /// <typeparam name="TContext">Contexto o qual terá a migração aplicada.</typeparam>
        /// <param name="builder">Objeto referenciado.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Operação assíncrona.</returns>
        public static async Task ApplyMigrationsAsync<TContext>(this IApplicationBuilder builder, CancellationToken cancellationToken = default) where TContext : DbContext
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                using (var services = scope.ServiceProvider.CreateScope())
                {
                    var logger = services.ServiceProvider.GetRequiredService<ILogger<IApplicationBuilder>>();
                    logger.LogInformation(Resource.LOG_RUNNING_CONTEXT_MIGRATION, typeof(TContext).Name);
                    var context = services.ServiceProvider.GetRequiredService<TContext>();

                    if (context.Database.IsRelational())
                    {
                        await context.Database.MigrateAsync(cancellationToken);
                    }
                }
            }
        }
    }
}
