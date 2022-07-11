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
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de retorno de código de status HTTP que inclui uma enumeração de validações.
    /// </summary>
    public sealed class ValidationStatusCodeEndpointResult : AbstractEndpointResult, IEndpointResult
    {
        private readonly ValidationStatusCodeModel _responseModel;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status. Veja <see cref="HttpStatusCode"/></param>
        /// <param name="validations">Enumeração de validações.</param>
        public ValidationStatusCodeEndpointResult(HttpStatusCode statusCode, IEnumerable<ValidationModel> validations) : this((int)statusCode, validations)
        {

        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status. Veja <see cref="HttpStatusCode"/></param>
        /// <param name="message">Mensagem de status.</param>
        /// <param name="validations">Enumeração de validações.</param>
        public ValidationStatusCodeEndpointResult(HttpStatusCode statusCode, string message, IEnumerable<ValidationModel> validations) : this((int)statusCode, message, validations)
        {

        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status. Veja <see cref="HttpStatusCode"/></param>
        /// <param name="message">Mensagem de status.</param>
        /// <param name="validations">Enumeração de validações.</param>
        /// <param name="details">Detalhes de ocorrência.</param>
        public ValidationStatusCodeEndpointResult(HttpStatusCode statusCode, string message, IEnumerable<ValidationModel> validations, object details) : this((int)statusCode, message, validations, details)
        {

        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status.</param>
        /// <param name="validations">Enumeração de validações.</param>
        public ValidationStatusCodeEndpointResult(int statusCode, IEnumerable<ValidationModel> validations) : this(statusCode, Resource.ResourceManager.GetString($"STATUS_CODE_{statusCode}", Resource.Culture), validations, null)
        {

        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status.</param>
        /// <param name="message">Mensagem de status.</param>
        /// <param name="validations">Enumeração de validações.</param>
        public ValidationStatusCodeEndpointResult(int statusCode, string message, IEnumerable<ValidationModel> validations) : this(statusCode, message, validations, null)
        {

        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status.</param>
        /// <param name="message">Mensagem de status.</param>
        /// <param name="validations">Enumeração de validações.</param>
        /// <param name="details">Detalhes de ocorrência.</param>
        public ValidationStatusCodeEndpointResult(int statusCode, string message, IEnumerable<ValidationModel> validations, object details)
        {
            this._responseModel = new ValidationStatusCodeModel()
            {
                Status = statusCode,
                Title = message,
                Validations = validations,
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
