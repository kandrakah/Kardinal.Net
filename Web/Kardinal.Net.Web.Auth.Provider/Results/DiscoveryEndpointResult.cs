using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web.Auth.Provider
{
    public class DiscoveryEndpointResult : AbstractEndpointResult, IEndpointResult
    {
        /// <summary>
        /// Dicionário de entradas do discovery.
        /// </summary>
        public IDictionary<string, object> Entries { get; }

        /// <summary>
        /// Valor máximo de idade do discovery.
        /// </summary>
        public int? MaxAge { get; }

        public DiscoveryEndpointResult(IDictionary<string, object> discovery, int? maxAge = null)
        {
            this.Entries = discovery;
            this.MaxAge = maxAge;
        }

        /// <summary>
        /// Método que executa a rotina de resposta do endpoint.
        /// </summary>
        /// <param name="context">Contexto http associado à requisição.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        public Task ExecuteAsync(HttpContext context, CancellationToken cancellationToken = default)
        {
            if (MaxAge.HasValue && MaxAge.Value >= 0)
            {
                context.Response.SetCache(MaxAge.Value, "Origin");
            }

            return context.Response.WriteJsonAsync(this.Entries, cancellationToken);
        }
    }
}
