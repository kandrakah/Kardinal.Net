using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Kardinal.Net.Web.Auth.Provider
{
    /// <summary>
    /// Implementação do serviço de perfil de usuários.
    /// </summary>
    /// <typeparam name="TUser">Tipo do objeto do perfil de usuário.</typeparam>
    public class ProfileService<TUser> : AbstractService, IProfileService where TUser : class
    {
        /// <summary>
        /// The claims factory.
        /// </summary>
        protected readonly IUserClaimsPrincipalFactory<TUser> _claimsFactory;

        /// <summary>
        /// The user manager.
        /// </summary>
        protected readonly UserManager<TUser> _userManager;

        public ProfileService(IServiceProvider provider) : base(provider)
        {
            this._claimsFactory = this.GetService<IUserClaimsPrincipalFactory<TUser>>();
            this._userManager = this.GetService<UserManager<TUser>>();
        }

        /// <summary>
        /// Método que busca um usuário pelo seu Id.
        /// </summary>
        /// <param name="userId">Id do usuário à ser buscado.</param>
        /// <returns>Dados do usuário relativo ao Id informado ou nulo se nenhuma correspondência for encontrada.</returns>
        public virtual async Task<TUser> FindByIdAsync(string userId)
        {
            var user = await this._userManager.FindByIdAsync(userId);
            if (user == null)
            {
                this._logger.LogWarning("No user found matching Id: {subjectId}", userId);
            }

            return user;
        }
    }
}
