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

using Microsoft.Extensions.Logging;
using System;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe base para todos os serviços da aplicação.
    /// </summary>
    public abstract class AbstractService : IDisposable
    {
        /// <summary>
        /// Instância do serviço de logs.
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Instância do provedor de serviços da aplicação.
        /// </summary>
        protected readonly IServiceProvider _provider;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="provider">Instância do provedor de serviços da aplicação.</param>
        public AbstractService(IServiceProvider provider)
        {
            this._provider = provider;

            var loggerFactory = this._provider.GetKardinalService<ILoggerFactory>();
            this._logger = loggerFactory.CreateLogger(this.GetType().Name);
        }

        /// <summary>
        /// Método que obtém a instância de um serviço.
        /// </summary>
        /// <typeparam name="T">Interface do serviço solicitada.</typeparam>
        /// <returns>Instância do serviço solicitado.</returns>
        protected T GetService<T>()
        {
            var result = this._provider.GetKardinalService<T>();
            return result;
        }

        /// <summary>
        /// Método que obtém a instância de um serviço.
        /// </summary>
        /// <param name="service">Tipo do serviço solicitado.</param>
        /// <returns>Instância do serviço solicitado.</returns>
        protected object GetService(Type service)
        {
            var result = this._provider.GetKardinalService(service);
            return result;
        }

        /// <summary>
        /// Método para liberação de recursos usados pelo serviço.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Método para liberação de recursos usados pelo serviço.
        /// </summary>
        /// <param name="disposing">Define se propriedades filhas também devem ser liberadas.</param>
        protected virtual void Dispose(bool disposing)
        {

        }
    }
}
