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
using System.Net;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Implementação padrão do serviço de manipulação de exceções de endpoints.
    /// </summary>
    internal class DefaultEndpointExceptionHandler : IEndpointExceptionHandler
    {
        private readonly ILogger _logger;

        internal DefaultEndpointExceptionHandler(ILogger logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Método invocado para manipular a exceção.
        /// </summary>
        /// <param name="context">ontexto http associado à requisição.</param>
        /// <param name="exception">Exceção lançada à ser manipulada.</param>
        public Task HandleAsync(HttpContext context, Exception exception)
        {
            var environment = context.GetEnvironment();

            var details = default(dynamic);
            if (environment == EnvironmentType.Development)
            {
                details = new
                {
                    Exception = exception.Message,
                    Stacktrace = exception.StackTrace
                };
            }

            var result = default(IEndpointResult);
            if (exception is ValidationException vEx)
            {
                result = new ValidationStatusCodeEndpointResult(HttpStatusCode.BadRequest, vEx.Message, vEx.Validations, details);
            }
            else if (exception is ServiceException sEx)
            {
                var message = string.IsNullOrEmpty(sEx.StatusMessage) ? sEx.Message : sEx.StatusMessage;
                result = new StatusCodeEndpointResult(sEx.StatusCode, message, details);
            }
            else
            {
                result = new StatusCodeEndpointResult(HttpStatusCode.InternalServerError, Localization.Resource.STATUS_CODE_500, details);
            }


            this._logger.LogError(exception, exception.Message);
            context.Response.SetNoCache();
            return result.ExecuteAsync(context);
        }
    }
}
