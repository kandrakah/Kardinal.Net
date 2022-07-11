using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web.Auth.Provider
{
    /// <summary>
    /// Implementação do manipulador do endpoint de descoberta.
    /// </summary>
    internal class DiscoveryHandler : IEndpointHandler
    {
        /// <summary>
        /// Instância do serviço de logs. Veja <see cref="ILogger"/>
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="logger">Instância do serviço de logs. Veja <see cref="ILogger"/></param>
        public DiscoveryHandler(ILogger<DiscoveryHandler> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Método responsável pelo processamento da requisição enviada ao endpoint.
        /// </summary>
        /// <param name="context">Contexto http associado à requisição.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Resultado do processamento do endpoint. Veja <see cref="IEndpointResult"/></returns>
        public async Task<IEndpointResult> ProcessAsync(HttpContext context, CancellationToken cancellationToken = default)
        {
            if (!HttpMethods.IsGet(context.Request.Method))
            {
                return new StatusCodeEndpointResult(HttpStatusCode.MethodNotAllowed);
            }

            this._logger.LogDebug("Generating discovery data");
            var discovery = await this.GenerateDiscoveryAsync();
            var result = new DiscoveryEndpointResult(discovery, 0);
            return result;
        }

        /// <summary>
        /// Método que gera o documento de descoberta do serviço de autenticação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dicionário de parâmetros do documento de descoberta.</returns>
        private async Task<IDictionary<string, object>> GenerateDiscoveryAsync(CancellationToken cancellationToken =default)
        {
            var entries = new Dictionary<string, object>
            {
                { OidcConstants.Discovery.Issuer, "" }
            };

            entries.Add(OidcConstants.Discovery.SubjectTypesSupported, new[]
            {
                "public"
            });

            entries.Add(OidcConstants.Discovery.CodeChallengeMethodsSupported, new[]
            {
                OidcConstants.CodeChallengeMethods.Plain,
                OidcConstants.CodeChallengeMethods.Sha256
            });

            await Task.CompletedTask;

            return entries;
        }
    }
}
