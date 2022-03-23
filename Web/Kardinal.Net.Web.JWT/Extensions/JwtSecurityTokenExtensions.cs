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

using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Extensões para <see cref="JwtSecurityToken"/>
    /// </summary>
    public static class JwtSecurityTokenExtensions
    {
        /// <summary>
        /// Extensão para obter um valor do token.
        /// </summary>
        /// <param name="token">Objeto referenciado.</param>
        /// <param name="type">Tipo da informação requerida.</param>
        /// <returns>Valor da informação requerida.</returns>
        public static string GetValue(this JwtSecurityToken token, [NotNull] string type)
        {
            var value = token.Claims.Where(x => x.Type == type).Select(x => x.Value).FirstOrDefault();
            return value;
        }
    }
}
