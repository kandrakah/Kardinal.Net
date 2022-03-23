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
using System.Data;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe de extensões para <see cref="DataTable"/>.
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Extensão para converter o Datatable para lista.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto à ser parseado</typeparam>
        /// <param name="table">Objeto referenciado</param>
        /// <returns>Lista proveniente do DataTable atual</returns>
        public static IEnumerable<T> Enumerate<T>(this DataTable table) where T : class, new()
        {
            var data = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                var item = row.GetItem<T>();
                data.Add(item);
            }
            return data;
        }

        /// <summary>
        /// Extensão para converter o Datatable para lista.
        /// </summary>
        /// <param name="type">Tipo de objeto à ser parseado</param>
        /// <param name="table">Objeto referenciado</param>
        /// <returns>Lista proveniente do DataTable atual</returns>
        public static IEnumerable<object> Enumerate(this DataTable table, Type type)
        {
            var data = new List<object>();
            foreach (DataRow row in table.Rows)
            {
                var item = row.GetItem(type);
                data.Add(item);
            }
            return data;
        }
    }
}
