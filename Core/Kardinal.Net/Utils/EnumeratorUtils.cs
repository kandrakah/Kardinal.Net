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
using System.Collections.Generic;
using System.Linq;

namespace Kardinal.Net
{
    /// <summary>
    /// Utilidades para enumerator.
    /// </summary>
    public static class EnumeratorUtils
    {
        /// <summary>
        /// Cria uma lista de valores de um enumerador.
        /// </summary>
        /// <typeparam name="T">Tipo do enumerador.</typeparam>
        /// <returns>Enumeração de valores de um enumerador.</returns>
        public static IEnumerable<T> ToList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        /// <summary>
        /// Método para obter um enumerador com sua descrição.
        /// </summary>
        /// <typeparam name="T">Tipo do enumerador.</typeparam>
        /// <returns>Enumeração de enums com suas descrições.</returns>
        public static IEnumerable<DescriptedEnumerator<T>> GetWithDescription<T>() where T : Enum
        {
            var enumType = typeof(T);
            var values = Enum.GetValues(enumType);

            var items = new List<DescriptedEnumerator<T>>();
            foreach (T value in values)
            {
                var item = new DescriptedEnumerator<T>()
                {
                    DisplayName = value.GetDescription(),
                    Value = value
                };
                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// Método para obter um enumerador com sua descrição.
        /// </summary>
        /// <typeparam name="T">Tipo do enumerador.</typeparam>
        /// <returns>Enumeração de enums com suas descrições.</returns>
        public static IEnumerable<DescriptedEnumerator<T>> GetWithDescription<T>(params T[] values) where T : Enum
        {
            var items = new List<DescriptedEnumerator<T>>();
            foreach (T value in values)
            {
                var item = new DescriptedEnumerator<T>()
                {
                    DisplayName = value.GetDescription(),
                    Value = value
                };
                items.Add(item);
            }

            return items;
        }
    }
}
