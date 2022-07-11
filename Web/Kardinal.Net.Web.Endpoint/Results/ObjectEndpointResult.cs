using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de retorno simples de código de status HTTP com um objeto como resposta.
    /// </summary>
    public class ObjectEndpointResult<T> : AbstractEndpointResult, IEndpointResult where T : class
    {
        /// <summary>
        /// Código de status de resposta.
        /// </summary>
        private readonly int _statusCode;

        /// <summary>
        /// Conteúdo da resposta.
        /// </summary>
        private readonly T _content;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="content"></param>
        public ObjectEndpointResult(HttpStatusCode statusCode, [NotNull] T content) : this((int)statusCode, content)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="content"></param>
        public ObjectEndpointResult(int statusCode, [NotNull] T content)
        {
            this._statusCode = statusCode;
            this._content = content;
        }

        /// <summary>
        /// Método que executa a rotina de resposta do endpoint.
        /// </summary>
        /// <param name="context">Contexto http associado à requisição.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        public async Task ExecuteAsync(HttpContext context, CancellationToken cancellationToken = default)
        {
            await this.ResultAsJsonAsync(context, this._statusCode, this._content, cancellationToken);
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return this._statusCode.ToString();
        }
    }
}
