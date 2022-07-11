using Kardinal.Net.Web.Auth.Provider;
using Microsoft.Extensions.DependencyInjection;

namespace Kardinal.Net.Web.Auth
{
    public static class IAuthServerBuilderExtensions
    {
        public static IAuthServerBuilder AddPermissionsStore<TImplementation>(this IAuthServerBuilder builder) where TImplementation : class, IPermissionsStore
        {
            builder.Services.AddScoped<IPermissionsStore, TImplementation>();
            return builder;
        }

        public static IAuthServerBuilder AddIdentityEndpointsHandlers(this IAuthServerBuilder builder)
        {
            builder.Services.AddEndpoint<DiscoveryHandler>("Discovery", "/.well-known/openid-configuration");
            return builder;
        }

        public static IAuthServerBuilder AddAuthServerRemoteManagement(this IAuthServerBuilder builder)
        {
            builder.Services.AddEndpoint<DiscoveryHandler>("Create User", "/management/users");
            return builder;
        }
    }
}
