using Kardinal.Net.Web.Auth.Provider;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Kardinal.Net.Web.Auth
{
    /// <summary>
    /// Instância do construtor do serviço de identidade.
    /// </summary>
    public interface IAuthServerBuilder
    {
        /// <summary>
        /// Instância da coleção de serviços.
        /// </summary>
        IServiceCollection Services { get; }
    }
}
