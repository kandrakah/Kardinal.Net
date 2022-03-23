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

using System.Data.Common;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe de extensões para <see cref="DbCommand"/>.
    /// </summary>
    public static class DbCommandExtensions
    {
        /// <summary>
        /// Extensão que adiciona um novo parâmetro ao DbCommand.
        /// </summary>
        /// <typeparam name="T">Tipo de valor do parâmetro.</typeparam>
        /// <param name="cmd">Este Objeto.</param>
        /// <param name="key">Chave do parâmetro</param>
        /// <param name="value">Valor do parâmetro</param>        
        public static void AddParameter<T>(this DbCommand cmd, string key, T value)
        {
            var dbParameter = cmd.CreateParameter();
            dbParameter.ParameterName = key;
            dbParameter.Value = value;
            cmd.Parameters.Add(dbParameter);
        }
    }
}
