using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Interface do middleware responsável pela gestão de manipuladores de endpoints.
    /// </summary>
    public interface IEndpointHandlerMiddleware
    {
        /// <summary>
        /// Método de invoação do middleware.
        /// </summary>
        /// <param name="context">Contexto http associado à requisição.</param>
        Task Invoke(HttpContext context);
    }
}
