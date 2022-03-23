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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Kardinal.Net
{
    /// <summary>
    /// Extensões para <see cref="int"/>.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Extensão que verifica se o valor está entre dois valores informados.
        /// </summary>
        /// <param name="value">Objeto referenciado</param>
        /// <param name="minValue">Valor mínimo</param>
        /// <param name="MaxValue">Valor máximo</param>
        /// <param name="includeLimits">Indica se os valores das extremidades devem ser incluidos na verificação</param>
        /// <returns>Resultado da verificação</returns>
        public static bool IsBetween(this int value, int minValue, int MaxValue, bool includeLimits = true)
        {
            return includeLimits ? value >= minValue && value <= MaxValue : value > minValue && value < MaxValue;
        }
    }
}
