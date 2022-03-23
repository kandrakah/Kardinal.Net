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

using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web.Authorization
{
    /// <summary>
    /// Classe de extensões para <see cref="KardinalIdentityOptions"/>.
    /// </summary>
    public static class AuthorityConfigurationsExtensions
    {
        /// <summary>
        /// Extensão que retorna os parâmetros de validação de token OAuth.
        /// </summary>
        /// <param name="options">Objeto referenciado</param>
        /// <param name="token">Token de autenticação</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Parâmetros de validação do Token. Veja <see cref="TokenValidationParameters"/></returns>
        internal static async Task<TokenValidationParameters> GetTokenParametesAsync(this KardinalIdentityOptions options, JwtSecurityToken token, CancellationToken cancellationToken = default)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
                    IssuerSigningKey = await options.GetSigningKeyAsync(token, cancellationToken),
                    ValidateIssuer = options.ValidateIssuer,
                    ValidateAudience = options.ValidateAudience,
                    ValidateLifetime = options.ValidateLifetime,
                    ClockSkew = TimeSpan.Zero
                };

                return tokenValidationParameters;
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método para obtenção da chave de assinatura do token provida pela autoridade de identificação.
        /// </summary>
        /// <param name="configurations">Objeto referenciado</param>
        /// <param name="token">Token de autenticação</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Chave de segurança provida pela autoridade de identificação.</returns>
        internal static async Task<SecurityKey> GetSigningKeyAsync(this KardinalIdentityOptions configurations, JwtSecurityToken token, CancellationToken cancellationToken = default)
        {
            var client = new HttpClient();
            try
            {
                var discovery = await client.GetDiscoveryDocumentAsync(configurations.Authority, cancellationToken);
                var kid = token.Header.Kid;
                var key = discovery.KeySet.Keys.Where(x => x.Kid == kid).FirstOrDefault();
                var json = JsonConvert.SerializeObject(key);
                var result = JsonWebKey.Create(json);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
