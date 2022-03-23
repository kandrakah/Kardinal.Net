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

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web.Authorization
{
    /// <summary>
    /// Extensões para <see cref="HttpClient"/>
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Extensão para busca dos dados do usuário junto à autoridade.
        /// </summary>
        /// <param name="client">Objeto referenciado</param>
        /// <param name="authorityUri">Endereço da autoridade.</param>
        /// <param name="token">Token de acesso do usuário.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados do usuário</returns>
        public static async Task<UserInfo> GetUserInfoAsync(this HttpClient client, string authorityUri, string token, CancellationToken cancellationToken = default)
        {
            try
            {
                client.BaseAddress = new Uri(authorityUri);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync("connect/userinfo", cancellationToken);
                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync(cancellationToken);
                    var userInfo = JsonConvert.DeserializeObject<UserInfo>(json);
                    return userInfo;
                }
                else
                {
                    var content = await result.Content.ReadAsStringAsync(cancellationToken);
                    throw new AuthorizationException(result.StatusCode, content);
                }
            }
            catch (AuthorizationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
