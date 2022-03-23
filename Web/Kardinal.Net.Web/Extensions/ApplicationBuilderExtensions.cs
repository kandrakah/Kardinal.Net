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

using Microsoft.AspNetCore.Builder;
using Serilog.Context;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de extensões para <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Extens serilog logs with additional information like remote IP
        /// address
        /// </summary>
        /// <param name="app">Instance of <see cref="IApplicationBuilder"/>.
        /// </param>
        public static void UseSerilog(this IApplicationBuilder app)
        {
            app.Use(async (ctx, next) =>
            {
                var remoteIpAddress = ctx.Request.HttpContext.Connection.RemoteIpAddress;

                using (LogContext.PushProperty("RemoteIpAddress", remoteIpAddress))
                {
                    await next();
                }
            });
        }
    }
}
