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
using System;
using System.Net;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de exceção causada por serviço não encontrado.
    /// </summary>
    public class ServiceNotFoundException : ServiceException
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        public ServiceNotFoundException(string message) : base(message)
        {
            this.StatusCode = HttpStatusCode.InternalServerError;
            this.StatusMessage = message;
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="type">Tipo de classe responsável pela exceção.</param>
        public ServiceNotFoundException(Type type) : this(Resource.ERROR_SERVICE_NOT_FOUND.SetParameters("SERVICE_NAME", type.Name))
        {

        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return this.Message;
        }
    }
}
