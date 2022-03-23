/*
Kardinal.Net
Copyright(C) 2022 Marcelo O.Mendes


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

using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kardinal.Net.Web.Authorization
{
    /// <summary>
    /// Classe de extensões para <see cref="AuthorizationFilterContext"/>.
    /// </summary>
    public static class AuthorizationFilterContextExtensions
    {
        /// <summary>
        /// Método que busca as claims do usuário atual.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options">Configurações do provedor de identidade.</param>
        /// <returns>Enumeração de claims do usuário atual.</returns>
        public static async Task<IEnumerable<Claim>> GetIdentityCurrentUserClaims(this AuthorizationFilterContext context, KardinalIdentityOptions options)
        {
            var token = context.GetAuthorizationToken("Bearer");
            var client = new HttpClient();
            var userInfo = await client.GetUserInfoAsync(options.Authority, token);
            var claims = userInfo.ToClaims();
            return claims;
        }
    }
}
