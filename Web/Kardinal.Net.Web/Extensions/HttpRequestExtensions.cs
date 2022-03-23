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

using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de extensões para <see cref="HttpRequest"/>
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Extensão para obtenção de um item do cabeçalho da requisição.
        /// </summary>
        /// <param name="request">Objeto referenciado.</param>
        /// <param name="key">Chave do item.</param>
        /// <returns>Valor do item.</returns>
        public static string GetHeader(this HttpRequest request, string key)
        {
            return request.Headers.Where(x => x.Key == key).Select(x => x.Value).FirstOrDefault();
        }
    }
}
