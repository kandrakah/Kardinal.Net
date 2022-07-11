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

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Interface do serviço de manipulamento de rotas de manipuladores de endpoints.
    /// </summary>
    public interface IEndpointRouteHandler
    {
        /// <summary>
        /// Método que busca o manipulador do endpoint da requisição.
        /// </summary>
        /// <param name="context">Contexto http associado à requisição.</param>
        /// <returns>Instância do maniplulador do endpoint. Veja <see cref="IEndpointHandler"/></returns>
        IEndpointHandler GetHandler(HttpContext context, out EndpointHandlerModel endpoint);
    }
}
