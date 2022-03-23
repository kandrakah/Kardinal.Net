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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kardinal.Net.Web.Authorization
{
    /// <summary>
    /// Implementação do serviço de autorização por permissões baseado em claims.
    /// </summary>
    public class ClaimsPermissionAuthorizationService : IPermissionAuthorizationService
    {
        /// <summary>
        /// Instância do serviço de log.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Instância do provedor de configurações.
        /// </summary>
        private readonly IOptions<PermissionsAuthorizationOptions> _options;

        /// <summary>
        /// Instância do serviço de dados do usuário atual.
        /// </summary>
        private readonly ICurrentUserService _currentUser;

        /// <summary>
        /// Acessor do contexto http.
        /// </summary>
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="logger">Instância do serviço de logs.</param>
        /// <param name="options">Instância do container de configurações.</param>
        /// <param name="currentUser">Instância do serviço do usuário do sistema.</param>
        /// <param name="accessor">Instância do accessor do contexto http.</param>
        public ClaimsPermissionAuthorizationService(ILogger<ClaimsPermissionAuthorizationService> logger, IOptions<PermissionsAuthorizationOptions> options, ICurrentUserService currentUser, IHttpContextAccessor accessor)
        {
            this._logger = logger;
            this._options = options;
            this._currentUser = currentUser;
            this._accessor = accessor;
        }

        /// <summary>
        /// Método que efetua a validação da autorização de acesso.
        /// </summary>
        /// <param name="context">Contexto de autorização</param>
        /// <param name="permissionRequeriment">Requerimento de permissão</param>
        public async Task AuthorizeAsync([NotNull] AuthorizationFilterContext context, [NotNull] PermissionsAuthorizationRequirement permissionRequeriment)
        {
            // Se permite anônimo, não é necessário validar nada.
            if (permissionRequeriment.ValidationType.Equals(PermissionValidationType.Annonymous))
            {
                this._logger.LogInformation(Resource.PERMISSION_AUTHORIZATION_ANNONYMOUS, this._accessor.HttpContext.Request.Path, this._currentUser.RemoteIpAddress);
                return;
            }

            var options = this._options.Value;

            // Obtendo token de autorização.
            var token = context.GetAuthorizationToken();

            var tokenDecoder = new JwtSecurityTokenHandler();

            this._logger.LogInformation(Resource.AUTHORIZATION_VALIDATING_TOKEN);

            // Verificando se é possível ler o token.
            if (!tokenDecoder.CanReadToken(token))
            {
                this._logger.LogWarning(Resource.AUTHORIZATION_ERROR_TOKEN_WRONG_FORMAT);
                context.Result = this.Unauthorized(Resource.AUTHORIZATION_ERROR_TOKEN_WRONG_FORMAT);
                return;
            }

            // Fazendo a leitura do token.
            var jwtToken = tokenDecoder.ReadJwtToken(token);

            // Efetuando a validação do token junto a autoridade.
            var result = await jwtToken.ValidateAsync(options.Identity);

            // Se o resultado não é válido, retorna 401.
            if (!result.IsValid)
            {
                this._logger.LogWarning(Resource.AUTHORIZATION_ERROR_INVALID_TOKEN, result.Status, result.Message);
                context.Result = this.Unauthorized(Resource.UNAUTHORIZED_INVALID_TOKEN);
                return;
            }

            // Se os dados do usuário são nulos, não possui identidade ou não está autenticado, retorna 401.
            if (result.Principal == null || !result.Principal.Identities.Any() || !result.Principal.Identity.IsAuthenticated)
            {
                this._logger.LogWarning(Resource.AUTHORIZATION_USER_UNHAUTHORIZED);
                context.Result = this.Unauthorized(Resource.AUTHORIZATION_USER_UNHAUTHORIZED);
                return;
            }

            this._logger.LogInformation(Resource.AUTHORIZATION_ADDING_USER_DETAILS);

            // Atualizando dados do usuário.            
            await this.UpdateUserIdentityWithAuthority(options.Identity, token);

            // Se é necessária apenas autenticação, não há porquê continuar.
            if (permissionRequeriment.ValidationType.Equals(PermissionValidationType.RequireAuthenticatedOnly))
            {
                this._logger.LogInformation(Resource.AUTHORIZATION_USER_AUTHORIZED, this._currentUser.DisplayName, this._accessor.HttpContext.Request.Path);
                return;
            }

            if (!this.ValidatePermissions(options.Claims, permissionRequeriment, out List<string> missingPermissions))
            {
                this.LogPermissionErrorMessage(permissionRequeriment.ValidationType, missingPermissions);
                context.Result = options.ShowMissingAuthorizationPermissions ? this.MissingPermissions(this._currentUser, missingPermissions) : this.Unauthorized(Resource.AUTHORIZATION_USER_UNHAUTHORIZED);
                return;
            }

            this._logger.LogInformation(Resource.AUTHORIZATION_USER_AUTHORIZED, this._currentUser.DisplayName, this._accessor.HttpContext.Request.Path);
        }

        /// <summary>
        /// Método que faz a atualização dos dados do usuário com informações da autoridade.
        /// </summary>
        /// <param name="settings">Configurações da autoridade</param>
        /// <param name="token">Token de acesso</param>
        private async Task UpdateUserIdentityWithAuthority([NotNull] KardinalIdentityOptions settings, [NotNull] string token)
        {
            try
            {
                var client = new HttpClient();
                var userInfo = await client.GetUserInfoAsync(settings.Authority, token);
                var claims = userInfo.ToClaims();
                var identity = new ClaimsIdentity(claims);
                this._accessor.HttpContext.User.AddIdentity(identity);
            }
            catch (Exception ex)
            {
                this._logger.LogError(Resource.AUTHORIZATION_ERROR_UPDATE_SYSTEM_USER, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Método que efetua a verificação de permissão de uma identidade considerando o requerimento de permissão informado.
        /// </summary>
        /// <param name="configurations"></param>
        /// <param name="permissionRequeriment">Requerimento de permissão</param>
        /// <param name="missingPermissions"></param>
        /// <returns>Verdadeiro caso o usuário atenda aos critérios requisitados e falso caso contrário</returns>
        public virtual bool ValidatePermissions([NotNull] ClaimsPermissionsOptions configurations, [NotNull] PermissionsAuthorizationRequirement permissionRequeriment, out List<string> missingPermissions)
        {
            var result = false;
            if (configurations.EnableRootPermission && this._currentUser.HasPermission(configurations.PermissionClaimName, configurations.RootPermissionKey))
            {
                this._logger.LogInformation(Resource.AUTHORIZATION_USER_ISROOT);
                missingPermissions = new List<string>();
                return true;
            }

            var usrPermissions = this._currentUser.GetClaims(configurations.PermissionClaimName).Select(x => x.Value).ToList();

            var ownedPermissions = permissionRequeriment.Permissions.Where(x => usrPermissions.Contains(x)).ToList();
            missingPermissions = permissionRequeriment.Permissions.Where(x => !usrPermissions.Contains(x)).ToList();

            result = permissionRequeriment.ValidationType switch
            {
                PermissionValidationType.RequireOneOf => ownedPermissions.Any(),
                PermissionValidationType.RequireAll => ownedPermissions.Count == permissionRequeriment.Permissions.Count,
                _ => true,
            };
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validationType"></param>
        /// <param name="missingPermissions"></param>
        private void LogPermissionErrorMessage(PermissionValidationType validationType, [NotNull] IEnumerable<string> missingPermissions)
        {
            var perms = string.Join(", ", missingPermissions);
            switch (validationType)
            {
                case PermissionValidationType.RequireOneOf:
                    this._logger.LogInformation(Resource.ERROR_PERMISSION_MISSING_SINGLE_LOGGER, this._currentUser.DisplayName, perms);
                    break;
                case PermissionValidationType.RequireAll:
                    this._logger.LogInformation(Resource.ERROR_PERMISSION_MISSING_MULTI_LOGGER, this._currentUser.DisplayName, perms);
                    break;
            }
        }

        /// <summary>
        /// Método que invoca o resultado de exceção exibindo as permissões ausentes
        /// que são requeridas para o acesso.
        /// </summary>
        /// <param name="currentUser">Dados do usuário atual.</param>
        /// <param name="permissions">Enumeração de permissões ausentes.</param>
        /// <returns>Resultado da ação.</returns>
        private IActionResult MissingPermissions([NotNull] ICurrentUserService currentUser, IEnumerable<string> permissions)
        {
            var details = new
            {
                MissingPermissions = permissions
            };

            var content = new StatusCodeModel()
            {
                Status = (int)HttpStatusCode.Unauthorized,
                Title = Resource.UNAUTHORIZED_MISSING_PERMISSIONS.SetParameters("userName", currentUser.DisplayName),
                Details = details
            };

            return new UnauthorizedObjectResult(content);
        }

        /// <summary>
        /// Método que invoca o resultado de exceção de requisição indicando
        /// acesso negado.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        /// <param name="details">Detalhes da exceção.</param>
        /// <returns>Resultado da ação.</returns>
        private IActionResult Unauthorized([NotNull] string message, object details = null)
        {
            var content = new StatusCodeModel()
            {
                Status = (int)HttpStatusCode.Unauthorized,
                Title = string.IsNullOrEmpty(message) ? Resource.UNAUTHORIZED_DEFAULT_MESSAGE : message,
                Details = details
            };
            return new UnauthorizedObjectResult(content);
        }
    }
}
