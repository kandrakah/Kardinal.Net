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

using Kardinal.Net.Web.Authorization.Permissions.Localization;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kardinal.Net.Web.Authorization
{
    /// <summary>
    /// Implementação do requerimento de autorização.
    /// </summary>
    public class PermissionsAuthorizationRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 
        /// </summary>
        public ICollection<string> Permissions { get; }

        /// <summary>
        /// Enumerador para indicar o tipo de validação de múltiplas permissões.
        /// </summary>
        public PermissionValidationType ValidationType { get; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="validationType">Tipo de validação de permissões.</param>
        /// <param name="requiredPermissions">Permissões requeridas para acesso.</param>
        public PermissionsAuthorizationRequirement(PermissionValidationType validationType = PermissionValidationType.RequireOneOf, params string[] requiredPermissions)
        {
            this.Permissions = requiredPermissions;
            this.ValidationType = validationType;

            switch (this.ValidationType)
            {
                case PermissionValidationType.Annonymous:
                    if (this.Permissions != null || this.Permissions.Any())
                    {
                        throw new ArgumentException(Resource.ERROR_PERMISSION_DECLARATION_NOT_REQUIRED);
                    }
                    break;
                case PermissionValidationType.RequireOneOf | PermissionValidationType.RequireAll:
                    if (this.Permissions == null || !this.Permissions.Any())
                    {
                        throw new ArgumentException(Resource.ERROR_PERMISSION_DECLARATION_REQUIRED);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Método que verifica se a permissão infomada existe no requerimento.
        /// </summary>
        /// <param name="permission">Nome da permissão à ser verificada</param>
        /// <returns>Verdadeiro caso a permissão exista no requerimento e falso caso contrário</returns>
        internal bool Contains(string permission)
        {
            return this.Permissions.Contains(permission);
        }
    }
}
