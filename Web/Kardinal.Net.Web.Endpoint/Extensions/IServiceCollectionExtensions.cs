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
        /// Extensão que adiciona o manipulador de rotas de endpoints.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddEndpointRouteHandler(this IServiceCollection services)
        {
            return services.AddEndpointRouteHandler<EndpointRouteHandler>();
        }

        /// <summary>
        /// Extensão que adiciona o manipulador de rotas de endpoints.
        /// </summary>
        /// <typeparam name="T">Tipo do manipulador de rotas de endpoints.</typeparam>
        /// <param name="services">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddEndpointRouteHandler<T>(this IServiceCollection services) where T : class, IEndpointRouteHandler
        {
            return services.AddTransient<IEndpointRouteHandler, T>();
        }

        /// <summary>
        /// Extensão que adiciona um manipulador de endpoint.
        /// </summary>
        /// <typeparam name="T">Tipo do manipulador de endpoint.</typeparam>
        /// <param name="services">Objeto referenciado.</param>
        /// <param name="name">Nome do endpoint.</param>
        /// <param name="route">Rota do endpoint.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddEndpoint<T>(this IServiceCollection services, string name, string route) where T : class, IEndpointHandler
        {
            services.AddTransient<T>();
            services.AddSingleton(new EndpointHandlerModel(name, route, typeof(T)));
            return services;
        }
    }
}
