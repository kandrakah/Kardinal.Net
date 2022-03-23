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

using Kardinal.Net.Web.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Extensões para <see cref="WebApplicationBuilder"/>
    /// </summary>
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Extensão para definir opções de uso do Kestrel como host da aplicação.
        /// </summary>
        /// <param name="builder">Objeto referenciado.</param>
        /// <param name="serviceName">Nome do serviço.</param>
        /// <param name="version">Versão do serviço.</param>
        /// <param name="args">Argumentos de inicialização.</param>
        /// <param name="configurationFiles"></param>
        /// <returns>Objeto referenciado.</returns>
        public static WebApplicationBuilder UseKardinal(this WebApplicationBuilder builder, [NotNull] string serviceName, KardinalVersion version, [NotNull] string[] args, params string[] configurationFiles)
        {
            Constants.Initialize(version, serviceName);

            builder.Configuration.AddJsonFile("appsettings.json", false);
            builder.Configuration.AddJsonFile($"appsettings.{builder.Environment}.json", true);
            builder.Configuration.AddEnvironmentVariables();
            builder.Configuration.AddCommandLine(args);

            var hostConfig = builder.Configuration.GetOptions<HostOptions>("Host");
            var urls = hostConfig.Hosts != null ? hostConfig.Hosts.ToArray() : Array.Empty<string>();

            if (hostConfig.UseIISIntegration && hostConfig.UseKestrel)
            {
                throw new ArgumentException(Resource.ERROR_CANT_USE_BOTH_KESTREL_IIS_INTEGRATION);
            }

            foreach (var config in configurationFiles)
            {
                builder.Configuration.AddJsonFile(config, true);
            }

            builder.WebHost.SuppressStatusMessages(true)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddSerilog(builder.Configuration);
                });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddApplicationInfoService();

            if (hostConfig.UseKestrel)
            {
                builder.UseKestrel(hostConfig);
            }

            if (hostConfig.UseIISIntegration)
            {
                builder.WebHost.UseIISIntegration();
            }

            if (urls.Any())
            {
                builder.WebHost.UseUrls(urls);
            }

            return builder;
        }

        /// <summary>
        /// Extensão para definir opções de uso do Kestrel como host da aplicação.
        /// </summary>
        /// <param name="builder">Objeto referenciado.</param>
        /// <param name="options">Opções de configuração do Kestrel.</param>
        /// <returns>Objeto referenciado.</returns>
        public static WebApplicationBuilder UseKestrel(this WebApplicationBuilder builder, HostOptions options)
        {
            builder.WebHost.UseKestrel(o =>
            {
                if (options.Certificate != null && options.Certificate.UseCertificate)
                {
                    var cert = default(X509Certificate2);
                    switch (options.Certificate.Storage)
                    {
                        case CertificateSource.FileSystem:
                            if (string.IsNullOrEmpty(options.Certificate.Path))
                            {
                                throw new ArgumentException(Resource.ERROR_NULL_EMPTY_CERTIFICATE_PATH);
                            }
                            if (!File.Exists(options.Certificate.Path))
                            {
                                throw new FileNotFoundException(Resource.ERROR_CERTIFICATE_NOT_FOUND.SetParameters("path", options.Certificate.Path));
                            }

                            cert = new X509Certificate2(options.Certificate.Path, options.Certificate.Password, X509KeyStorageFlags.DefaultKeySet);
                            if (!cert.HasPrivateKey)
                            {
                                throw new ArgumentException(Resource.ERROR_CERTIFICATE_PRIVATE_KEY_MISSING.SetParameters("thumbprint", cert.Thumbprint));
                            }
                            break;
                        case CertificateSource.Storage:
                            using (var store = new X509Store(options.Certificate.StoreName, options.Certificate.StoreLocation, OpenFlags.ReadOnly))
                            {
                                var result = store.Certificates.Find(X509FindType.FindByThumbprint, options.Certificate.Thumbprint, options.Certificate.ValidOnly);
                                if (result.Count <= 0)
                                {
                                    throw new FileNotFoundException(Resource.ERROR_CERTIFICATE_NOT_FOUND.SetParameters("path", $"[{options.Certificate.StoreName}|{options.Certificate.StoreLocation}]"));
                                }

                                cert = result[0];
                                if (!cert.HasPrivateKey)
                                {
                                    throw new ArgumentException(Resource.ERROR_CERTIFICATE_PRIVATE_KEY_MISSING.SetParameters("thumbprint", cert.Thumbprint));
                                }
                            }
                            break;
                    }

                    if (cert != null)
                    {
                        o.ConfigureHttpsDefaults(x =>
                        {
                            x.ServerCertificate = cert;
                        });
                    }
                }
            });
            return builder;
        }        
    }
}
