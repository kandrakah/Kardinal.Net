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
using System.Linq;

namespace Kardinal.Net
{
    /// <summary>
    /// Extensões para array.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Extensão para randonizar ordem do vetor.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto contido no vetor.</typeparam>
        /// <param name="values">Objeto referenciado.</param>
        /// <returns>Itens cuja ordem foi randonizada.</returns>
        public static T[] Shuffle<T>(this T[] values)
        {
            return values.OrderBy(x => Guid.NewGuid()).ToArray();
        }
    }
}
