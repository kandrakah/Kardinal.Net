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

using Kardinal.Net.Web.Localization;
using System.Globalization;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// 
    /// </summary>
    public static class StatusCodeUtils
    {
        /// <summary>
        /// Método que traz a descrição de um código de status.
        /// </summary>
        /// <param name="statusCode">Código de status</param>
        /// <param name="culture">Dados do idioma desejado</param>
        /// <returns>Descrição do código de status</returns>
        public static string GetDescription(int statusCode, CultureInfo culture = null)
        {
            if (culture == null)
            {
                culture = Resource.Culture;
            }
            var value = Resource.ResourceManager.GetString($"STATUS_CODE_{statusCode}", culture);
            return string.IsNullOrEmpty(value) ? Resource.STATUS_CODE_UNKNOW.SetParameters("VALUE", statusCode) : value;
        }
    }
}
