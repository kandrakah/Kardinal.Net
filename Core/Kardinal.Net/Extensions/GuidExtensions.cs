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

namespace Kardinal.Net
{
    /// <summary>
    /// Extensões para <see cref="Guid"/>.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Extensão que faz a comparação entre dois Guids para verificar sua igualdade.
        /// </summary>
        /// <param name="value">Objeto referenciado</param>
        /// <param name="comparedValue">Guid à ser comparado</param>
        /// <param name="ignoreIfNull">Ignorar verificação se o valor Guid atual for nulo</param>
        /// <returns>Resultado da comparação</returns>
        public static bool Equals(this Guid? value, Guid? comparedValue, bool ignoreIfNull = false)
        {
            if (ignoreIfNull && value == null)
            {
                return true;
            }
            else
            {
                return value.Equals(comparedValue);
            }
        }

        /// <summary>
        /// Extensão que transforma este Guid em uma representação string em caixa alta.
        /// </summary>
        /// <param name="guid">Este Objeto</param>
        /// <returns>Representação String em caixa alta.</returns>
        public static string ToUpper(this Guid guid)
        {
            return guid.ToString().ToUpper();
        }

        /// <summary>
        /// Extensão que transforma este Guid em uma representação string em caixa alta.
        /// </summary>
        /// <param name="guid">Este Objeto</param>
        /// <param name="format">Formato de string desejada.</param>
        /// <returns>Representação String em caixa alta.</returns>
        public static string ToUpper(this Guid guid, string format)
        {
            return guid.ToString(format).ToUpper();
        }

        /// <summary>
        /// Extensão que transforma este Guid em uma representação string em caixa alta.
        /// </summary>
        /// <param name="guid">Este Objeto</param>
        /// <param name="format">Formato de string desejada.</param>
        /// <param name="provider">Provedor de formatação.</param>
        /// <returns>Representação String em caixa alta.</returns>
        public static string ToUpper(this Guid guid, string format, IFormatProvider provider)
        {
            return guid.ToString(format, provider).ToUpper();
        }
    }
}
