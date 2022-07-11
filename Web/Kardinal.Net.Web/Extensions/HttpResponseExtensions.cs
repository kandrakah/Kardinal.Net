/*
Kardinal.Net
Copyright (C) 2022 Marcelo O. Mendes

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with this program; if not, write to the Free Software Foundation,
Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Extensões para <see cref="HttpResponse"/>
    /// </summary>
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Extensão que escreve a 
        /// </summary>
        /// <param name="response">Objeto referenciado.</param>
        /// <param name="data">Dados à serem serializados.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        public static Task WriteJsonAsync<T>(this HttpResponse response, T data, CancellationToken cancellationToken = default)
        {
            return response.WriteJsonAsync(data, $"{MediaTypeNames.Application.Json}; charset=UTF-8", cancellationToken);
        }

        /// <summary>
        /// Extensão que escreve a 
        /// </summary>
        /// <param name="response">Objeto referenciado.</param>
        /// <param name="data">Dados à serem serializados.</param>
        /// <param name="contentType">Tipo do conteúdo serializado.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        public static Task WriteJsonAsync<T>(this HttpResponse response, T data, string contentType, CancellationToken cancellationToken = default)
        {
            var options = response.HttpContext.RequestServices.GetKardinalService<IOptions<MvcNewtonsoftJsonOptions>>(null);

            var json = options != null ? JsonConvert.SerializeObject(data, options.Value.SerializerSettings) : JsonConvert.SerializeObject(data);
            response.ContentType = contentType;
            return response.WriteAsync(json, cancellationToken);
        }
    }
}
