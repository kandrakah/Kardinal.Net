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

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de extensões para ServiceProvider.
    /// </summary>
    public static class IServiceProviderExtensions
    {
        /// <summary>
        /// Extensão para verificar se um serviço está registrado no pool de serviços.
        /// </summary>
        /// <param name="provider">Objeto referenciado.</param>
        /// <param name="service">Tipo do serviço buscado.</param>
        /// <returns>Verdadeiro caso o serviço exista e falso caso contrário.</returns>
        public static bool ServiceExists(this IServiceProvider provider, Type service)
        {
            try
            {
                var result = provider.GetService(service);
                return result != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Extensão para verificar se um serviço está registrado no pool de serviços.
        /// </summary>
        /// <typeparam name="T">Tipo do serviço buscado.</typeparam>
        /// <param name="provider">Objeto referenciado.</param>
        /// <returns>Verdadeiro caso o serviço exista e falso caso contrário.</returns>
        public static bool ServiceExists<T>(this IServiceProvider provider)
        {
            try
            {
                var result = provider.GetService<T>();
                return result != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Extensão que obtém um serviço do pool de serviços.
        /// </summary>
        /// <param name="provider">Objeto referenciado.</param>
        /// <param name="service">Tipo do serviço buscado.</param>
        /// <param name="replacementService">Instância de serviço de substituição caso o serviço solicitado não seja encontrado.</param>
        /// <returns>Instância do serviço buscado.</returns>
        public static object GetKardinalService(this IServiceProvider provider, Type service, object replacementService = null)
        {
            var result = provider.GetService(service);
            if (result == null)
            {
                if (replacementService == null)
                {
                    throw new ServiceNotFoundException(service);
                }
                result = replacementService;
            }
            return result;
        }

        /// <summary>
        /// Extensão que obtém um serviço do pool de serviços.
        /// </summary>
        /// <typeparam name="T">Tipo do serviço buscado.</typeparam>
        /// <param name="provider">Objeto referenciado.</param>
        /// <param name="replacementService">Instância de serviço de substituição caso o serviço solicitado não seja encontrado.</param>
        /// <returns>Instância do serviço buscado.</returns>
        public static T GetKardinalService<T>(this IServiceProvider provider, T replacementService = default)
        {
            var result = provider.GetService<T>();

            if (result == null)
            {
                if (replacementService == null)
                {
                    throw new ServiceNotFoundException(typeof(T));
                }
                result = replacementService;
            }
            return result;
        }

        /// <summary>
        /// Extensão que obtém o serviço de informações do servidor.
        /// </summary>
        /// <param name="provider">Objeto referenciado.</param>
        /// <returns>Instância do serviço <see cref="IApplicationInfoService"/></returns>
        public static IApplicationInfoService GetServerInfoService(this IServiceProvider provider)
        {
            return provider.GetKardinalService<IApplicationInfoService>();
        }

        /// <summary>
        /// Extensão que obtém o serviço de log.
        /// </summary>
        /// <typeparam name="T">Tipo da classe invocadora do logger.</typeparam>
        /// <param name="provider">Objeto referenciado.</param>
        /// <returns>Instância do serviço <see cref="ILogger"/> referente à classe solicitante.</returns>
        public static ILogger<T> GetLoggerService<T>(this IServiceProvider provider) where T : class
        {
            return provider.GetKardinalService<ILogger<T>>(new DefaultLogger<T>());            
        }

        /// <summary>
        /// Extensão que obtém o serviço do usuário atual.
        /// </summary>
        /// <param name="provider">Objeto referenciado.</param>
        /// <returns>Instância do serviço do usuário atual.</returns>
        public static ICurrentUserService GetCurrentUser(this IServiceProvider provider)
        {
            return provider.GetKardinalService<ICurrentUserService>();
        }

        /// <summary>
        /// Extensão para obter opções.
        /// </summary>
        /// <typeparam name="T">Tipo da opção desejada.</typeparam>
        /// <param name="provider">Objeto referenciado.</param>
        /// <returns>Opções solicitadas.</returns>
        public static T GetOptions<T>(this IServiceProvider provider) where T : class, new()
        {
            var options = provider.GetService<IOptions<T>>();
            return options?.Value ?? Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Extensão para obter o serviço de fila de tarefas em segundo plano.
        /// </summary>
        /// <param name="provider">Objeto referenciado.</param>
        /// <returns>Instância do serviço de fila de tarefas em segundo plano.</returns>
        public static IBackgroundTaskQueue GetBackgroundTaskQueueService(this IServiceProvider provider)
        {
            return provider.GetKardinalService<IBackgroundTaskQueue>();
        }
    }
}
