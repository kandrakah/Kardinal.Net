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

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Implementação do middleware responsável pela gestão de manipuladores de endpoints.
    /// </summary>
    public class EndpointHandlerMiddleware : IEndpointHandlerMiddleware
    {
        /// <summary>
        /// Instância do delegador de requisições. Veja <see cref="RequestDelegate"/>
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Instância do manipulador de rotas. Veja <see cref="IEndpointRouteHandler"/>
        /// </summary>
        private readonly IEndpointRouteHandler _routeHandler;

        /// <summary>
        /// Instância do manipulador e exceções de endpoints. Veja <see cref="IEndpointExceptionHandler"/>
        /// </summary>
        private readonly IEndpointExceptionHandler exceptionHandler;

        /// <summary>
        /// Instância do serviço de logs. Veja <see cref="ILogger"/>
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="next">Instância do delegador de requisições. Veja <see cref="RequestDelegate"/></param>
        /// <param name="routeHandler">Instância do manipulador de rotas. Veja <see cref="IEndpointRouteHandler"/></param>
        /// <param name="exceptionHandler">Instância do manipulador e exceções de endpoints. Veja <see cref="IEndpointExceptionHandler"/></param>
        /// <param name="logger">Instância do serviço de logs. Veja <see cref="ILogger"/></param>
        public EndpointHandlerMiddleware(RequestDelegate next, IEndpointRouteHandler routeHandler, ILogger<EndpointHandlerMiddleware> logger, IEndpointExceptionHandler exceptionHandler = null)
        {
            this._next = next;
            this._routeHandler = routeHandler;
            this._logger = logger;
            this.exceptionHandler = exceptionHandler ?? new DefaultEndpointExceptionHandler(logger);
            this._logger.LogTrace("Using endpoint handlher middleware.");
        }

        /// <summary>
        /// Método de invoação do middleware.
        /// </summary>
        /// <param name="context">Contexto http associado à requisição.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var endpoint = this._routeHandler.GetHandler(context, out EndpointHandlerModel model);
                if (endpoint != null)
                {
                    _logger.LogDebug("Using endpoint handler: {endpointType} for {url}", model.Name, context.Request.Path.ToString());

                    var result = await endpoint.ProcessAsync(context);
                    if (result != null)
                    {
                        _logger.LogDebug("The handler {type} completed the request with the response: {response}", model.Name, result.GetType().FullName);
                        await result.ExecuteAsync(context);
                    }

                    return;
                }
            }
            catch (Exception ex)
            {
                await this.exceptionHandler.HandleAsync(context, ex);
                return;
            }

            await _next(context);
        }
    }
}
