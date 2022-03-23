/*
Kardinal.Net
Copyright(C) 2022 Marcelo O.Mendes


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

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Kardinal.Net.Web.Authorization
{
    /// <summary>
    /// Extensões para <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extensão para adição do serviço de autorização por permissões baseado em claims.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <param name="configuration">Instância do container de configurações.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddClaimsPermissionAuthorizationService(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetOptions<PermissionsAuthorizationOptions>();
            return services.AddClaimsPermissionAuthorizationService(options);
        }

        /// <summary>
        /// Extensão para adição do serviço de autorização por permissões baseado em claims.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <param name="configuration">Instância do container de configurações.</param>
        /// <param name="section">Nome da sessão de configurações.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddClaimsPermissionAuthorizationService(this IServiceCollection services, IConfiguration configuration, string section)
        {
            var options = configuration.GetOptions<PermissionsAuthorizationOptions>(section);
            return services.AddClaimsPermissionAuthorizationService(options);
        }

        /// <summary>
        /// Extensão para adição do serviço de autorização por permissões baseado em claims.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <param name="options">Opções do serviço de permissões.</param>        
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddClaimsPermissionAuthorizationService(this IServiceCollection services, PermissionsAuthorizationOptions options)
        {
            return services.AddPermissionAuthorizationService<ClaimsPermissionAuthorizationService, PermissionsAuthorizationOptions>(options);
        }

        /// <summary>
        /// Extensão para adição do serviço de autorização por permissões baseado em claims.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <param name="options">Opções do serviço de permissões.</param>        
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddClaimsPermissionAuthorizationService(this IServiceCollection services, Action<PermissionsAuthorizationOptions> options)
        {
            return services.AddPermissionAuthorizationService<ClaimsPermissionAuthorizationService, PermissionsAuthorizationOptions>(options);
        }

        /// <summary>
        /// Extensão para adição do serviço de autorização por permissões.
        /// </summary>
        /// <typeparam name="TImplementation">Implementação do serviço de autorização por permissões. Veja <see cref="IPermissionAuthorizationService"/></typeparam>
        /// <typeparam name="TOptions">Tipo das configurações do serviço de autorização por permissões.</typeparam>
        /// <param name="services">Objeto referenciado.</param>
        /// <param name="options">Configurações do serviço de autorização por permissões.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddPermissionAuthorizationService<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation, TOptions>(this IServiceCollection services, TOptions options) where TImplementation : class, IPermissionAuthorizationService where TOptions : class
        {
            var action = new Action<TOptions>(o => o = options);
            return services.AddPermissionAuthorizationService<TImplementation, TOptions>(action);
        }

        /// <summary>
        /// Extensão para adição do serviço de autorização por permissões.
        /// </summary>
        /// <typeparam name="TImplementation">Implementação do serviço de autorização por permissões. Veja <see cref="IPermissionAuthorizationService"/></typeparam>
        /// <typeparam name="TOptions">Tipo das configurações do serviço de autorização por permissões.</typeparam>
        /// <param name="services">Objeto referenciado.</param>
        /// <param name="options">Configurações do serviço de autorização por permissões.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddPermissionAuthorizationService<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation, TOptions>(this IServiceCollection services, Action<TOptions> options) where TImplementation : class, IPermissionAuthorizationService where TOptions : class
        {
            services.AddScoped<IPermissionAuthorizationService, TImplementation>();
            services.Configure(options);
            return services;
        }
    }
}
