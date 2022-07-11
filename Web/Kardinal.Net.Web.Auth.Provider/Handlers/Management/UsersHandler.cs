using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web.Auth.Provider
{
    public class UsersHandler : IEndpointHandler
    {
        /// <summary>
        /// Método responsável pelo processamento da requisição enviada ao endpoint.
        /// </summary>
        /// <param name="context">Contexto http associado à requisição.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Resultado do processamento do endpoint. Veja <see cref="IEndpointResult"/></returns>
        public async Task<IEndpointResult> ProcessAsync(HttpContext context, CancellationToken cancellationToken = default)
        {
            return null;
        }
    }
}
