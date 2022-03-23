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

using Kardinal.Net.Web.Authorization.Localization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Kardinal.Net.Web.Authorization
{
    /// <summary>
    /// Classe que representa o resultado de uma validação de token.
    /// </summary>
    public class KardinalTokenValidationResult
    {
        /// <summary>
        /// Atributo que indica se o token é válido.
        /// </summary>
        public bool IsValid => this.Status == TokenValidationStatus.Valid;

        /// <summary>
        /// Atributo que indica o status da validação.
        /// </summary>
        public TokenValidationStatus Status { get; }

        /// <summary>
        /// Atributo que indica a mensagem de validação.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Atributo que contém os dados do usuário presentes no token.
        /// </summary>
        public ClaimsPrincipal Principal { get; set; }

        /// <summary>
        /// Atributo que contém os dados validados do token.
        /// </summary>
        public SecurityToken SecurityToken { get; set; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="status">Status da validação.</param>
        /// <param name="message">Mensagem de validação.</param>
        public KardinalTokenValidationResult(TokenValidationStatus status, string message = null)
        {
            this.Status = status;
            this.Message = !string.IsNullOrEmpty(message) ? message : Resource.TOKEN_VALIDATION_VALID;
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            var message = this.Status != TokenValidationStatus.Valid ? this.Message : string.Empty;
            return $"[{this.Status}]{message}";
        }
    }
}
