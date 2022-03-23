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
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe de extensões para <see cref="DataRow"/>.
    /// </summary>
    public static class DataRowExtensions
    {
        /// <summary>
        /// Extensão para converter o um DataRow em um objeto.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto à ser parseado</typeparam>
        /// <param name="dataRow">Objeto referenciado</param>
        /// <returns>Objeto proveniente do DataRow</returns>
        public static T GetItem<T>(this DataRow dataRow) where T : class, new()
        {
            var type = typeof(T);
            var instance = Activator.CreateInstance<T>();
            FillObject(dataRow, type, instance);
            return instance;
        }

        /// <summary>
        /// Extensão para converter o um DataRow em um objeto.
        /// </summary>
        /// <param name="dataRow">Objeto referenciado</param>
        /// <param name="type">Tipo do objeto à ser convertido.</param>
        /// <returns>Objeto proveniente do DataRow</returns>
        public static object GetItem(this DataRow dataRow, Type type)
        {
            var instance = Activator.CreateInstance(type);
            FillObject(dataRow, type, instance);
            return instance;
        }

        /// <summary>
        /// Método que preenche o objeto com os dados do DataRow.
        /// </summary>
        /// <param name="dataRow">Instância do DataRow.</param>
        /// <param name="type">Tipo do objeto à ser preenchido.</param>
        /// <param name="instance">Instância do objeto à ser preenchido.</param>
        private static void FillObject([NotNull] DataRow dataRow, [NotNull] Type type, [NotNull] object instance)
        {
            foreach (DataColumn column in dataRow.Table.Columns)
            {
                foreach (PropertyInfo info in type.GetProperties())
                {
                    if (info.Name == column.ColumnName)
                    {
                        if (info.PropertyType.IsEnum)
                        {
                            info.SetValue(instance, Enum.Parse(info.PropertyType, (string)dataRow[column.ColumnName]), null);
                        }
                        else
                        {
                            info.SetValue(instance, dataRow[column.ColumnName], null);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }
}
