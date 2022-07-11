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

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de extensões para <see cref="IServiceCollection"/>.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Extensão para adição de serviço de informações de sistema.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddApplicationInfoService(this IServiceCollection services)
        {
            services.AddSingleton<IApplicationInfoService, ApplicationInfoService>();
            return services;
        }

        /// <summary>
        /// Extensão para adição do usuário de sistema.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddBackgroundTaskHostedService(this IServiceCollection services)
        {
            return services.AddBackgroundTaskHostedService(o =>
            {
                o.BackgrounTaskQueueCapacity = 1000;
                o.BackgroundTaskFullMode = System.Threading.Channels.BoundedChannelFullMode.Wait;
            });
        }

        /// <summary>
        /// Extensão para adição do usuário de sistema.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <param name="options">Configurações do serviço de fila.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddBackgroundTaskHostedService(this IServiceCollection services, Action<BackgroundTaskServiceOptions> options)
        {
            services.Configure(options);
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddHostedService<BackgroundTaskHostedService>();
            return services;
        }
    }
}
