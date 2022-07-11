using Microsoft.AspNetCore.Builder;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Extensões para <see cref="IApplicationBuilder"/>
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Extensão que registra o Middleware de manipulação de endpoints.
        /// </summary>
        /// <param name="builder">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IApplicationBuilder UseEndpointHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseEndpointHandlerMiddleware<EndpointHandlerMiddleware>();
        }

        /// <summary>
        /// Extensão que registra o Middleware de manipulação de endpoints.
        /// </summary>
        /// <typeparam name="T">Tipo do middleware de manipulação de endpoints.</typeparam>
        /// <param name="builder">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IApplicationBuilder UseEndpointHandlerMiddleware<T>(this IApplicationBuilder builder) where T : class, IEndpointHandlerMiddleware
        {
            return builder.UseMiddleware<T>();
        }
    }
}
