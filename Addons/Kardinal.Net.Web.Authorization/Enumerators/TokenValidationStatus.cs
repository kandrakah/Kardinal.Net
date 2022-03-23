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

namespace Kardinal.Net.Web.Authorization
{
    /// <summary>
    /// Enumerador do resultado da validação do token.
    /// </summary>
    public enum TokenValidationStatus
    {
        /// <summary>
        /// Token válido
        /// </summary>
        Valid,

        /// <summary>
        /// Token expirado
        /// </summary>
        Expired,

        /// <summary>
        /// Audiência inválida
        /// </summary>
        InvalidAudience,

        /// <summary>
        /// Token vencido
        /// </summary>
        InvalidLifetime,

        /// <summary>
        /// Assinatura inválida
        /// </summary>
        InvalidSignature,

        /// <summary>
        /// Sem data de expiração.
        /// </summary>
        NoExpiration,

        /// <summary>
        /// Início de validade posterior a atual.
        /// </summary>
        NotYetValid,

        /// <summary>
        /// 
        /// </summary>
        ReplayAdd,

        /// <summary>
        /// 
        /// </summary>
        ReplayDetected,

        /// <summary>
        /// Falha geral
        /// </summary>
        Error,
    }
}
