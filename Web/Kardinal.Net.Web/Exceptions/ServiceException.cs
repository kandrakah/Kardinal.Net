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

using System;
using System.Net;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de exceção de serviços de Api.
    /// </summary>
    public class ServiceException : KardinalException
    {
        /// <summary>
        /// Código de status da exceção.
        /// </summary>
        public HttpStatusCode StatusCode { get; protected set; }

        /// <summary>
        /// Mensagem de status da exceção.
        /// </summary>
        public string StatusMessage { get; protected set; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        public ServiceException() : base()
        {
            this.StatusCode = HttpStatusCode.InternalServerError;
            this.StatusMessage = this.StatusCode.GetDescription();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        public ServiceException(string message) : base(message)
        {
            this.StatusCode = HttpStatusCode.InternalServerError;
            this.StatusMessage = message;
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status da exceção.</param>
        /// <param name="message">Mensagem de exceção.</param>
        /// <param name="innerException">Exceção interior.</param>
        protected ServiceException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            this.StatusCode = statusCode;
            this.StatusMessage = message;
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status da exceção.</param>
        public ServiceException(HttpStatusCode statusCode) : this(statusCode, statusCode.GetDescription())
        {
            this.StatusCode = statusCode;
            this.StatusMessage = statusCode.GetDescription();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status da exceção.</param>
        /// <param name="description">Indica se deve ser adicionado o código de erro à descrição.</param>
        public ServiceException(HttpStatusCode statusCode, string description) : base(description)
        {
            this.StatusCode = statusCode;
            this.StatusMessage = description;
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return $"[{(int)this.StatusCode}]{this.StatusMessage}";
        }
    }
}
