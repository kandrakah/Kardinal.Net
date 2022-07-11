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

using Kardinal.Net.Web.Endpoint.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Implementação do serviço de manipulamento de rotas de manipuladores de endpoints.
    /// </summary>
    public class EndpointRouteHandler : IEndpointRouteHandler
    {
        /// <summary>
        /// Enumeração de manipuladores de endpoints registrados.
        /// </summary>
        private readonly IEnumerable<EndpointHandlerModel> _handlers;

        /// <summary>
        /// Instância do serviço de logs.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="handlers">Enumeração de manipuladores de endpoints registrados.</param>
        /// <param name="logger">Instância do serviço de logs.</param>
        public EndpointRouteHandler(IEnumerable<EndpointHandlerModel> handlers, ILogger<EndpointRouteHandler> logger)
        {
            this._logger = logger;
            this._handlers = handlers;
        }

        /// <summary>
        /// Método que busca o manipulador do endpoint da requisição.
        /// </summary>
        /// <param name="context">Contexto http associado à requisição.</param>
        /// <param name="endpoint">Dados do manipulador do endpoint.</param>
        /// <returns>Instância do maniplulador do endpoint. Veja <see cref="IEndpointHandler"/></returns>
        public IEndpointHandler GetHandler(HttpContext context, out EndpointHandlerModel endpoint)
        {
            if (context == null)
            {
                throw new ArgumentNullException(Resource.ERROR_HTTP_CONTEXT_NULL);
            }

            var handler = default(IEndpointHandler);
            endpoint = this._handlers.Where(x => x.Path.Equals(context.Request.Path, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            try
            {
                if (endpoint != null)
                {
                    var service = context.RequestServices.GetService(endpoint.Handler);
                    if (service is IEndpointHandler)
                    {
                        _logger.LogDebug("Endpoint enabled: {endpoint}, successfully created handler: {endpointHandler}", endpoint.Name, endpoint.Handler.FullName);
                        handler = service as IEndpointHandler;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Endpoint enabled: {endpoint}, failed to create handler: {endpointHandler}", endpoint.Name, endpoint.Handler.FullName);
            }

            return handler;
        }
    }
}
