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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Extensões para <see cref="HttpContext"/>
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Extensão que obtém o ambiente de execução da aplicação.
        /// </summary>
        /// <param name="context">Objeto referenciado.</param>
        /// <returns>Tipo do ambiente de execução.</returns>
        public static EnvironmentType GetEnvironment(this HttpContext context)
        {
            var hostEnvronment = context.RequestServices.GetKardinalService<IHostEnvironment>();
            if (!string.IsNullOrEmpty(hostEnvronment.EnvironmentName))
            {
                switch (hostEnvronment.EnvironmentName.ToUpper())
                {
                    case "DEVELOPMENT":
                        return EnvironmentType.Development;
                    case "STAGING":
                        return EnvironmentType.Staging;
                    case "PRODUCTION":
                        return EnvironmentType.Production;
                    default:
                        return EnvironmentType.Development;
                }
            }
            else
            {
                var configuration = context.RequestServices.GetKardinalService<IConfiguration>();
                var options = configuration.GetOptions<EnvironmentOptions>();
                if(options != null)
                {
                    return options.Environment;
                }
                else
                {
                    return EnvironmentType.Development;
                }
            }
        }
    }
}
