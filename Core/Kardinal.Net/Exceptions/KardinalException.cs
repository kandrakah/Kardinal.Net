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
using System.Runtime.Serialization;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe de exceção de base da biblioteca.
    /// </summary>
    public class KardinalException : Exception
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        public KardinalException() : base() { }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        public KardinalException(string message) : base(message) { }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        /// <param name="innerException">Exceção interior.</param>
        public KardinalException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="info">O <see cref="SerializationInfo"/> que contém os dados do objeto serializado sobre a exceção que está sendo lançada.</param>
        /// <param name="context">O <see cref="StreamingContext"/> que contém informações contextuais sobre a origem ou destino.</param>
        public KardinalException(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
