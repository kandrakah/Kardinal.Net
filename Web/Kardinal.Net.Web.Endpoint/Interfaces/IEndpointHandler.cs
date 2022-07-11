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
    /// Interface do manipulador de endpoint.
    /// </summary>
    public interface IEndpointHandler
    {
        /// <summary>
        /// Método responsável pelo processamento da requisição enviada ao endpoint.
        /// </summary>
        /// <param name="context">Contexto http associado à requisição.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Resultado do processamento do endpoint. Veja <see cref="IEndpointResult"/></returns>
        Task<IEndpointResult> ProcessAsync(HttpContext context, CancellationToken cancellationToken = default);
    }
}
