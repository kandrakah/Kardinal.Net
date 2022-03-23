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
using System.ComponentModel;

namespace Kardinal.Net
{
    /// <summary>
    /// Extensões para <see cref="Enum"/>
    /// </summary>
    public static class EnumeratorExtensions
    {
        /// <summary>
        /// Extensão para obter a descrição do enumerador definida pelo atributo <see cref="DescriptionAttribute"/>.
        /// </summary>
        /// <param name="enumerator">Objeto referenciado.</param>
        /// <returns>Descrição do enumerador.</returns>
        public static string GetDescription(this Enum enumerator)
        {
            var type = enumerator.GetType();
            var memInfo = type.GetMember(enumerator.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return enumerator.ToString();
        }
    }
}
