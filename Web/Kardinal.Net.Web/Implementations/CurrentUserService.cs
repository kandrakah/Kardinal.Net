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
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe que representa um usuário ativo no sistema.
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        public string Sub => this.GetSub();

        /// <summary>
        /// Atributo que representa o nome de usuário.
        /// </summary>
        public string Username => this.GetUsername();

        /// <summary>
        /// Atributo que representa o nome apresentável do usuário.
        /// </summary>
        public string DisplayName => this.GetDisplayName();

        /// <summary>
        /// Identificador único do cliente utilizado pelo usuário.
        /// </summary>
        public string ClientId => this.GetClientId();

        /// <summary>
        /// Atributo que indica se o usuário está autenticado.
        /// </summary>
        public bool IsAuthenticated => this.Authenticated();

        /// <summary>
        /// Atributo que representa o Ip local do usuário.
        /// </summary>
        public string LocalIpAddress => this.GetLocalIpAddress();

        /// <summary>
        /// Atributo que representa o Ip remoto do usuário.
        /// </summary>
        public string RemoteIpAddress => this.GetRemoteIpAddress();

        /// <summary>
        /// Instância das configurações.
        /// </summary>
        private readonly CurrentUserOptions _options;

        /// <summary>
        /// Accessor
        /// </summary>
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// Identidade principal do usuário.
        /// </summary>
        private ClaimsPrincipal Principal => this._accessor.HttpContext?.User;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="configurations">Instância do container de configurações.</param>
        /// <param name="accessor">Instância do acessor</param>
        public CurrentUserService(IConfiguration configurations, IHttpContextAccessor accessor)
        {
            this._options = configurations.GetOptions<CurrentUserOptions>();
            this._accessor = accessor;
        }

        /// <summary>
        /// Método que obtém uma claim do usuário de um tipo declarado. 
        /// Caso hajam várias claims do mesmo tipo, a primeira será retornada.
        /// </summary>
        /// <param name="type">Tipo da claim desejada.</param>
        /// <returns>Claim referente ao tipo solicitado ou null caso a mesma não seja encontrada.</returns>
        public Claim GetClaim([NotNull] string type)
        {
            return !string.IsNullOrEmpty(type) ? this.Principal?.FindFirst(type) : null;
        }

        /// <summary>
        /// Método que obtém uma enumeração de claims do usuário de um tipo declarado.
        /// </summary>
        /// <param name="type">tipo das claims desejadas</param>
        /// <returns>Enumeração de claims do tipo solicitado.</returns>
        public IEnumerable<Claim> GetClaims([NotNull] string type)
        {
            return !string.IsNullOrEmpty(type) ? this.Principal?.FindAll(type) : Enumerable.Empty<Claim>();
        }

        /// <summary>
        /// Método que obtém o id único do usuário.
        /// </summary>
        /// <returns></returns>
        private string GetSub()
        {
            return this.GetClaim(this._options.Claims.Sub)?.Value;
        }

        /// <summary>
        /// Método que obtém o nome de usuário.
        /// </summary>
        /// <returns>Nome de usuário.</returns>
        private string GetUsername()
        {
            var preferedUsername = this.GetClaim(this._options.Claims.PreferedUsername)?.Value;
            if (!string.IsNullOrEmpty(preferedUsername))
            {
                return preferedUsername;
            }

            var username = this.GetClaim(this._options.Claims.Username)?.Value;
            if (!string.IsNullOrEmpty(username))
            {
                return username;
            }

            var nickName = this.GetClaim(this._options.Claims.Nickname)?.Value;
            if (!string.IsNullOrEmpty(nickName))
            {
                return nickName;
            }

            var name = this.GetClaim(this._options.Claims.Name)?.Value;
            if (!string.IsNullOrEmpty(name))
            {
                return name;
            }

            var sub = this.GetSub();
            if (!string.IsNullOrEmpty(sub))
            {
                return sub;
            }

            return string.Empty;
        }

        /// <summary>
        /// Método que obtém o nome apresentável do usuário.
        /// </summary>
        /// <returns>Nome apresentável do usuário.</returns>
        private string GetDisplayName()
        {
            var preferedUsername = this.GetClaim(this._options.Claims.Name)?.Value;
            if (!string.IsNullOrEmpty(preferedUsername))
            {
                return preferedUsername;
            }

            var givenName = this.GetClaim(this._options.Claims.GivenName)?.Value;
            var familyName = this.GetClaim(this._options.Claims.FamilyName)?.Value;
            if (!string.IsNullOrEmpty(givenName))
            {
                return $"{givenName} {familyName}".Trim();
            }

            return this.GetUsername();
        }

        /// <summary>
        /// Método que obtém o Id único do cliente usado pelo usuário.
        /// </summary>
        /// <returns>Id único do cliente do usuário.</returns>
        private string GetClientId()
        {
            return this.GetClaim(this._options.Claims.ClientId)?.Value;
        }

        /// <summary>
        /// Método que traz se o usuário está autenticado.
        /// </summary>
        /// <returns></returns>
        private bool Authenticated()
        {
            return this.Principal != null && this.Principal.Identity != null && this.Principal.Identity.IsAuthenticated;
        }

        /// <summary>
        /// Método que obtém o Ip local da conexão.
        /// </summary>
        /// <returns>Ip local da conexão.</returns>
        private string GetLocalIpAddress()
        {
            return this._accessor.HttpContext.Connection.LocalIpAddress.ToString();
        }

        /// <summary>
        /// Método que obtém o Ip remoto da conexão.
        /// </summary>
        /// <returns>Ip remoto da conexão.</returns>
        private string GetRemoteIpAddress()
        {
            return this._accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return $"[{this.Sub}]{this.DisplayName}";
        }
    }
}
