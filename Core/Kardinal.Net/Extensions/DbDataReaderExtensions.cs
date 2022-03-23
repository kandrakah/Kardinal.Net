﻿/*
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
using System.Data.Common;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe de extensões para <see cref="DbDataReader"/>.
    /// </summary>
    public static class DbDataReaderExtensions
    {
        /// <summary>
        /// Extensão que transforma um <see cref="DbDataReader"/> em um <see cref="DataTable"/>.
        /// </summary>
        /// <param name="reader">Objeto referenciado</param>
        /// <returns>DataTable proveniente do DataReader</returns>
        public static DataTable ToDataTable(this DbDataReader reader)
        {
            var schema = reader.GetSchemaTable();
            var result = new DataTable(schema.TableName);

            foreach (DataRow row in schema.Rows)
            {
                var columnName = row["ColumnName"].ToString();
                if (!result.Columns.Contains(columnName))
                {
                    var column = new DataColumn
                    {
                        ColumnName = columnName,
                        DataType = row["DataType"] as Type,
                        Unique = Convert.ToBoolean(row["IsUnique"]),
                        AllowDBNull = Convert.ToBoolean(row["AllowDBNull"]),
                        ReadOnly = Convert.ToBoolean(row["IsReadOnly"])
                    };
                    result.Columns.Add(column);
                }
            }

            while (reader.Read())
            {
                var row = result.NewRow();
                for (int i = 0; i < result.Columns.Count; i++)
                {
                    row[i] = reader.GetValue(i);
                }
                result.Rows.Add(row);
            }

            return result;
        }
    }
}
