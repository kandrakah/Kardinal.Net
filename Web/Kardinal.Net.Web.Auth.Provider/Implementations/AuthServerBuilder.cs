using Microsoft.Extensions.DependencyInjection;

namespace Kardinal.Net.Web.Auth
{
    public sealed class AuthServerBuilder : IAuthServerBuilder
    {
        public IServiceCollection Services { get; }

        internal AuthServerBuilder(IServiceCollection services)
        {
            if(services == null)
            {
                throw new KardinalException("A coleção de serviços é nula ou inválida.");
            }

            this.Services = services;
        }
    }
}
