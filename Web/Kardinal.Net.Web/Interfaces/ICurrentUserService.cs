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

using System.Collections.Generic;
using System.Security.Claims;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Interface que representa um usuário do sistema.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        string Sub { get; }

        /// <summary>
        /// Atributo que representa o nome de usuário.
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Atributo que representa o nome apresentável do usuário.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Identificador único do cliente utilizado pelo usuário.
        /// </summary>
        string ClientId { get; }

        /// <summary>
        /// Atributo que representa o Ip local do usuário.
        /// </summary>
        string LocalIpAddress { get; }

        /// <summary>
        /// Atributo que representa o Ip remoto do usuário.
        /// </summary>
        string RemoteIpAddress { get; }

        /// <summary>
        /// Atributo que indica se o usuário está autenticado.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Método que obtém uma claim do usuário de um tipo declarado. 
        /// Caso hajam várias claims do mesmo tipo, a primeira será retornada.
        /// </summary>
        /// <param name="type">Tipo da claim desejada.</param>
        /// <returns>Claim referente ao tipo solicitado ou null caso a mesma não seja encontrada.</returns>
        Claim GetClaim(string type);

        /// <summary>
        /// Método que obtém uma enumeração de claims do usuário de um tipo declarado.
        /// </summary>
        /// <param name="type">tipo das claims desejadas</param>
        /// <returns>Enumeração de claims do tipo solicitado.</returns>
        IEnumerable<Claim> GetClaims(string type);
    }
}
