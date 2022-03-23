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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace Kardinal.Net.Web.Authorization
{
    /// <summary>
    /// Classe de atributo que define a permissão necessária para acesso ao endpoint com ela decorado.
    /// </summary>
    public class PermissionAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        public PermissionAttribute() : this(PermissionValidationType.RequireAuthenticatedOnly) { }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="validationType">Tipo de validação de permissão. Veja <see cref="PermissionValidationType"/></param>
        /// <param name="permissions">Permissões requeridas para autorização</param>
        public PermissionAttribute(PermissionValidationType validationType, params string[] permissions) : base(typeof(PermissionAttributeImpl))
        {
            var arguments = new PermissionsAuthorizationRequirement(validationType, permissions);
            Arguments = new[] { arguments };
        }

        /// <summary>
        /// Implementação do atributo de permissão.
        /// </summary>
        private class PermissionAttributeImpl : Attribute, IAuthorizationFilter
        {
            private readonly ILogger _logger;
            private readonly PermissionsAuthorizationRequirement _permissionRequeriment;
            private readonly IServiceProvider _provider;

            /// <summary>
            /// Método construtor
            /// </summary>
            /// <param name="logger">Instância do serviço de log</param>
            /// <param name="permissionRequeriment">Permissões requeridas pelo atributo</param>
            /// <param name="provider">Instância do provedor de serviços</param>
            public PermissionAttributeImpl(ILogger<PermissionAttribute> logger, PermissionsAuthorizationRequirement permissionRequeriment, IServiceProvider provider)
            {
                this._logger = logger;
                this._permissionRequeriment = permissionRequeriment;
                this._provider = provider;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                try
                {
                    var service = this._provider.GetKardinalService<IPermissionAuthorizationService>();
                    service.AuthorizeAsync(context, this._permissionRequeriment).Wait();
                }
                catch (ServiceNotFoundException ex)
                {
                    _logger.LogError(ex, Resource.ERROR_PERMISSION_SERVICE_NOT_FOUND);
                    context.Result = new StatusCodeResult(500);
                }
            }
        }
    }
}
