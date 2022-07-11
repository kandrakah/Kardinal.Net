using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de modelo de parâmetros de endpoint.
    /// </summary>
    public sealed class EndpointRequestParametersModel
    {
        /// <summary>
        /// Contexto da conexão.
        /// </summary>
        public HttpContext Context { get; internal set; }

        /// <summary>
        /// Dicionário de parâmetros de rota.
        /// </summary>
        public IDictionary<string, object> Route { get; internal set; }

        /// <summary>
        /// Dicionário de parâmetros via query string.
        /// </summary>
        public QueryString QueryString { get; internal set; }        
    }
}
