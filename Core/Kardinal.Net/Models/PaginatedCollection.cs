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
using System.Linq;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe de modelo de enumeração de itens paginados.
    /// </summary>
    /// <typeparam name="T">Tipo do item da coleção.</typeparam>
    public class PaginatedCollection<T>
    {
        /// <summary>
        /// Enumerador de itens da coleção.
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Número total de itens da coleção.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        public PaginatedCollection()
        {
            this.Items = new List<T>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="items">Coleção dos objetos paginados.</param>
        /// <param name="total"></param>
        public PaginatedCollection([NotNull]IEnumerable<T> items, int total)
        {
            Items = items;
            Total = total;
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return $"{this.Items.Count()} / {this.Total}";
        }
    }
}
