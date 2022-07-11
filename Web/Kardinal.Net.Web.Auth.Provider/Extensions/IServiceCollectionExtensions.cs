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

using Kardinal.Net.Web.Auth.Provider;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Kardinal.Net.Web.Auth
{
    /// <summary>
    /// Classe de extensões para <see cref="IServiceCollection"/>.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        public static IAuthServerBuilder AddAuthServer(this IServiceCollection services)
        {
            var builder = services.AddAuthServerCore();
            return builder;
        }

        public static IAuthServerBuilder AddAuthServer(this IServiceCollection services, [NotNull] Action<AuthServerOptions> options)
        {
            var builder = services.AddAuthServerCore(options);
            return builder;
        }

        public static IAuthServerBuilder AddAuthServerCore(this IServiceCollection services)
        {
            var builder = new AuthServerBuilder(services);
            //builder.Services.AddScoped<ISystemClock, SystemClock>();            
            builder.Services.AddEndpointRouteHandler();
            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddOptions();
            builder.Services.AddHttpClient();

            builder.Services.AddAuthentication(AuthConstants.Cookies.DefaultAuthenticationScheme)
                .AddCookie(AuthConstants.Cookies.DefaultAuthenticationScheme)
                .AddCookie(AuthConstants.Cookies.ExternalAuthenticationScheme);

            //builder.Services.AddSingleton<IConfigureOptions<CookieAuthenticationOptions>, ConfigureInternalCookieOptions>();
            //builder.Services.AddSingleton<IPostConfigureOptions<CookieAuthenticationOptions>, PostConfigureInternalCookieOptions>();

            //builder.Services.AddTransient<IAuthenticationService, IdentityServerAuthenticationService>();
            //builder.Services.AddTransient<IAuthenticationHandlerProvider, FederatedSignoutAuthenticationHandlerProvider>();

            return builder;
        }

        public static IAuthServerBuilder AddAuthServerCore(this IServiceCollection services, [NotNull] Action<AuthServerOptions> options)
        {
            var builder = services.AddAuthServerCore();
            builder.Services.Configure(options);
            return builder;
        }
    }
}
