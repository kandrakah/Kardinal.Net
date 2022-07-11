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
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe abstrata de resultado.
    /// </summary>
    public abstract class AbstractEndpointResult
    {
        /// <summary>
        /// Método que escreve o objeto de resultado como json.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de resposta.</typeparam>
        /// <param name="context">Objeto referenciado.</param>
        /// <param name="statusCode">Código de status da resposta.</param>
        /// <param name="data">Dados da resposta.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        internal virtual async Task ResultAsJsonAsync<T>(HttpContext context, int statusCode, T data, CancellationToken cancellationToken = default)
        {
            context.Response.StatusCode = statusCode;
            await context.Response.WriteJsonAsync(data, cancellationToken);
            await context.Response.Body.FlushAsync(cancellationToken);
        }
    }
}
