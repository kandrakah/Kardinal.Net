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

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Security.Claims;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe que representa as informações de um usuário providas pela autoridade.
    /// </summary>
    public class UserInfo : Dictionary<string, object>
    {
        /// <summary>
        /// Método que converte este objeto em uma enumeração de claims. Veja <see cref="Claim"/>
        /// </summary>
        /// <returns>Enumeração de claims do usuário.</returns>
        internal IEnumerable<Claim> ToClaims()
        {
            var claims = new HashSet<Claim>();
            foreach (var item in this)
            {
                if (item.Value is string)
                {
                    claims.Add(new Claim(item.Key, item.Value.ToString()));
                }
                else if (item.Value is JArray)
                {
                    var array = item.Value as JArray;
                    foreach (var itm in array)
                    {
                        claims.Add(new Claim(item.Key, itm.ToString()));
                    }
                }
                else
                {
                    claims.Add(new Claim(item.Key, item.Value.ToString()));
                }
            }
            return claims;
        }
    }
}
