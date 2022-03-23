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

using System;
using System.Net;

namespace Kardinal.Net.Web.Authorization
{
    /// <summary>
    /// Exceção gerada pelo serviço de autorização.
    /// </summary>
    public class AuthorizationException : ServiceException
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        public AuthorizationException() : base()
        {
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        public AuthorizationException(string message) : base(message) { }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status da exceção.</param>
        /// <param name="message">Mensagem de exceção.</param>
        /// <param name="innerException">Exceção interior.</param>
        protected AuthorizationException(HttpStatusCode statusCode, string message, Exception innerException) : base(statusCode, message, innerException) { }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status da exceção.</param>
        public AuthorizationException(HttpStatusCode statusCode) : base(statusCode) { }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status da exceção.</param>
        /// <param name="description">Indica se deve ser adicionado o código de erro à descrição.</param>
        public AuthorizationException(HttpStatusCode statusCode, string description) : base(statusCode, description) { }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
