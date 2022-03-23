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

using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Kardinal.Net.Web.Authorization
{
    /// <summary>
    /// Classe de configurações Identity
    /// </summary>
    public class KardinalIdentityOptions : IdentityOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApiName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApiSecret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool SaveToken { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public SupportedTokens SupportedTokens { get; set; } = SupportedTokens.Both;

        /// <summary>
        /// 
        /// </summary>
        public string RoleClaimType { get; set; } = JwtClaimTypes.Role;

        /// <summary>
        /// 
        /// </summary>
        public string NameClaimType { get; set; } = JwtClaimTypes.Name;

        /// <summary>
        /// 
        /// </summary>
        public bool ValidateIssuerSigningKey { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool ValidateIssuer { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool ValidateAudience { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool ValidateLifetime { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool RequireHttpsMetadata { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, string> Scopes { get; set; } = new Dictionary<string, string>();
    }
}
