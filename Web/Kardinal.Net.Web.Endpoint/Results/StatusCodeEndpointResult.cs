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

using Kardinal.Net.Web.Localization;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de retorno simples de código de status HTTP.
    /// </summary>
    public sealed class StatusCodeEndpointResult : AbstractEndpointResult, IEndpointResult
    {
        private readonly StatusCodeModel _responseModel;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status. Veja <see cref="HttpStatusCode"/></param>
        public StatusCodeEndpointResult(HttpStatusCode statusCode) : this((int)statusCode)
        {

        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status. Veja <see cref="HttpStatusCode"/></param>
        /// <param name="message">Mensagem de status.</param>
        public StatusCodeEndpointResult(HttpStatusCode statusCode, string message) : this((int)statusCode, message)
        {

        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status. Veja <see cref="HttpStatusCode"/></param>
        /// <param name="message">Mensagem de status.</param>
        /// <param name="details">Detalhes de ocorrência.</param>
        public StatusCodeEndpointResult(HttpStatusCode statusCode, string message, object details) : this((int)statusCode, message, details)
        {

        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status.</param>
        public StatusCodeEndpointResult(int statusCode) : this(statusCode, Resource.ResourceManager.GetString($"STATUS_CODE_{statusCode}", Resource.Culture), null)
        {

        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status.</param>
        /// <param name="message">Mensagem de status.</param>
        public StatusCodeEndpointResult(int statusCode, string message) : this(statusCode, message, null)
        {

        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status.</param>
        /// <param name="message">Mensagem de status.</param>
        /// <param name="details">Detalhes de ocorrência.</param>
        public StatusCodeEndpointResult(int statusCode, string message, object details)
        {
            this._responseModel = new StatusCodeModel()
            {
                Status = statusCode,
                Title = message,
                Details = details
            };
        }

        /// <summary>
        /// Método que executa a rotina de resposta do endpoint.
        /// </summary>
        /// <param name="context">Contexto http associado à requisição.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        public async Task ExecuteAsync(HttpContext context, CancellationToken cancellationToken = default)
        {
            await this.ResultAsJsonAsync(context, this._responseModel.Status, this._responseModel, cancellationToken);
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return this._responseModel.ToString();
        }
    }
}
