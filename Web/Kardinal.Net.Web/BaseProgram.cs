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
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe base para inicialização simplificada do WebAPI.
    /// </summary>
    public abstract class BaseProgram
    {
        /// <summary>
        /// Fonte do token de cancelamento de operação assíncrona.
        /// </summary>
        protected readonly static CancellationTokenSource CancellationTokenSource = new();

        /// <summary>
        /// Instância do logger da aplicação.
        /// </summary>
        protected static ILogger Logger { get; set; }

        /// <summary>
        /// Método de inicialização do serviço web.
        /// </summary>
        /// <typeparam name="TStartup">Classe de inicialização do serviço.</typeparam>
        /// <param name="args">Argumentos de inicialização.</param>
        protected static void Initialize<TStartup>(string[] args) where TStartup : class
        {
            var ver = KardinalVersion.Parse(1, 0);
            Initialize<TStartup>(ver, Resource.DEFAULT_SERVICE_NAME, args);
        }

        /// <summary>
        /// Método de inicialização do serviço web.
        /// </summary>
        /// <typeparam name="TStartup">Classe de inicialização do serviço.</typeparam>
        /// <param name="version">Versão do serviço web. veja <see cref="KardinalVersion"/>.</param>
        /// <param name="description">Descrição serviço web.</param>
        /// <param name="args">Argumentos de inicialização.</param>
        /// <param name="additionalConfigurationFiles">Arquivos de configuração adicionais.</param>
        protected static void Initialize<TStartup>(KardinalVersion version, string description, string[] args, params string[] additionalConfigurationFiles) where TStartup : class
        {
            Constants.Initialize(version, description);
            Start<TStartup>(args, (services) =>
            {
                services.AddHttpContextAccessor();
                services.AddSingleton<IApplicationInfoService, ApplicationInfoService>();
                return;
            }, additionalConfigurationFiles);
        }

        /// <summary>
        /// Método de inicialização do serviço web.
        /// </summary>
        /// <typeparam name="TStartup">Classe de inicialização do serviço.</typeparam>
        /// <param name="args">Argumentos de inicialização.</param>
        /// <param name="configureServices">Serviços configurados à serem adicionados ao serviço.</param>
        /// <param name="additionalConfigurationFiles">Arquivos de configuração adicionais.</param>
        private static void Start<TStartup>(string[] args, Action<IServiceCollection> configureServices = null, params string[] additionalConfigurationFiles) where TStartup : class
        {
            string contentRoot = Environment.GetEnvironmentVariable("ASPNETCORE_CONTENTROOT");

            if (!string.IsNullOrWhiteSpace(contentRoot) && !string.IsNullOrEmpty(contentRoot))
            {
                var attr = File.GetAttributes(contentRoot);

                if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    throw new ArgumentException(Resource.ERROR_CONTENT_ROOT_INVALID.SetParameters("CONTENT_ROOT", contentRoot));
                }
            }
            else
            {
                contentRoot = AppDomain.CurrentDomain.BaseDirectory;
            }

            Start<TStartup>(args, contentRoot, configureServices, additionalConfigurationFiles);
        }

        /// <summary>
        /// Método de inicialização do serviço web.
        /// </summary>
        /// <typeparam name="TStartup">Classe de inicialização do serviço.</typeparam>
        /// <param name="args">Argumentos de inicialização.</param>
        /// <param name="basePath">Diretório base da aplicação.</param>
        /// <param name="configureServices">Serviços configurados à serem adicionados ao serviço.</param>
        /// <param name="additionalConfigurationFiles">Arquivos de configuração adicionais.</param>
        private static void Start<TStartup>(string[] args, string basePath, Action<IServiceCollection> configureServices = null, params string[] additionalConfigurationFiles) where TStartup : class
        {
            try
            {
                Logger = new DefaultLogger<BaseProgram>();
                var process = Process.GetCurrentProcess();
                Console.Title = $"{Constants.ApplicationName} - {Constants.Environment} - PID {process.Id}";

                var config = LoadConfiguration(args, basePath, additionalConfigurationFiles);
                var hostConfig = config.GetOptions<HostOptions>("Host");
                var urls = hostConfig.Hosts != null ? hostConfig.Hosts.ToArray() : Array.Empty<string>();

                var builder = WebHost.CreateDefaultBuilder(args);

                if (hostConfig.UseIISIntegration && hostConfig.UseKestrel)
                {
                    throw new ArgumentException(Resource.ERROR_CANT_USE_BOTH_KESTREL_IIS_INTEGRATION);
                }

                if (hostConfig.UseKestrel)
                {
                    builder.UseKestrel(o =>
                    {
                        if (hostConfig.Certificate != null && hostConfig.Certificate.UseCertificate)
                        {
                            var cert = default(X509Certificate2);
                            switch (hostConfig.Certificate.Storage)
                            {
                                case CertificateSource.FileSystem:
                                    if (string.IsNullOrEmpty(hostConfig.Certificate.Path))
                                    {
                                        throw new ArgumentException(Resource.ERROR_NULL_EMPTY_CERTIFICATE_PATH);
                                    }
                                    if (!File.Exists(hostConfig.Certificate.Path))
                                    {
                                        throw new FileNotFoundException(Resource.ERROR_CERTIFICATE_NOT_FOUND.SetParameters("path", hostConfig.Certificate.Path));
                                    }

                                    cert = new X509Certificate2(hostConfig.Certificate.Path, hostConfig.Certificate.Password, X509KeyStorageFlags.DefaultKeySet);
                                    if (!cert.HasPrivateKey)
                                    {
                                        throw new ArgumentException(Resource.ERROR_CERTIFICATE_PRIVATE_KEY_MISSING.SetParameters("thumbprint", cert.Thumbprint));
                                    }
                                    break;
                                case CertificateSource.Storage:
                                    using (var store = new X509Store(hostConfig.Certificate.StoreName, hostConfig.Certificate.StoreLocation, OpenFlags.ReadOnly))
                                    {
                                        var result = store.Certificates.Find(X509FindType.FindByThumbprint, hostConfig.Certificate.Thumbprint, hostConfig.Certificate.ValidOnly);
                                        if (result.Count <= 0)
                                        {
                                            throw new FileNotFoundException(Resource.ERROR_CERTIFICATE_NOT_FOUND.SetParameters("path", $"[{hostConfig.Certificate.StoreName}|{hostConfig.Certificate.StoreLocation}]"));
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
                }

                if (urls.Any())
                {
                    builder.UseUrls(urls);
                }

                builder.SuppressStatusMessages(true)
                 .UseContentRoot(basePath)
                 .UseConfiguration(config)
                 .ConfigureLogging((hostingContext, logging) =>
                 {
                     logging.AddSerilog(config);
                 })
                 .UseStartup<TStartup>();

                if (configureServices != null)
                {
                    builder = builder.ConfigureServices(configureServices);
                }

                if (hostConfig.UseIISIntegration)
                {
                    builder = builder.UseIISIntegration();
                }

                var host = builder.Build();
                Logger = host.Services.GetKardinalService<ILogger<BaseProgram>>();
                Logger.LogInformation(Resource.INITIALIZATION_APP_INFO, Constants.ApplicationName, Constants.Environment, process.Id);
                Logger.LogInformation(Resource.INITIALIZE_BASEPATH, basePath);
                if (hostConfig.UseIISIntegration)
                {
                    Logger.LogInformation(Resource.INITIALIZE_IIS_INTEGRATION);
                }

                Logger.LogInformation(Resource.INITIALIZATION_COMPLETE);
                foreach (var url in urls)
                {
                    Logger.LogInformation(Resource.LISTENING_URL, url);
                }
                var task = host.RunAsync(CancellationTokenSource.Token);
                task.Wait();
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, ex.Message);
                Shutdown();
            }
        }

        /// <summary>
        /// Método para o encerramento do serviço.
        /// </summary>
        /// <param name="exitCode">Código de saída da aplicação.</param>
        public static void Shutdown(int exitCode = 0)
        {
            Logger.LogInformation(Resource.FINALIZING_SERVICE);
            CancellationTokenSource.Cancel();
            Environment.ExitCode = exitCode;
            Logger.LogInformation(Resource.SERVICE_FINALIZED, exitCode);
            Environment.Exit(exitCode);
        }

        /// <summary>
        /// Método que efetua a leitura das configurações do serviço.
        /// </summary>
        /// <param name="args">Argumentos de inicialização.</param>
        /// <param name="basePath">Diretório base da aplicação.</param>
        /// <param name="additionalConfigurationFiles">Arquivos de configuração adicionais.</param>
        /// <returns>Instância de <see cref="IConfigurationRoot"/> contendo as configurações do serviço.</returns>
        private static IConfigurationRoot LoadConfiguration(string[] args, string basePath, params string[] additionalConfigurationFiles)
        {
            if (string.IsNullOrEmpty(basePath))
            {
                throw new ArgumentNullException(Resource.ERROR_CONTENT_ROOT_INVALID);
            }

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{Constants.Environment}.json", true, true);

            if (additionalConfigurationFiles != null)
            {
                foreach (var configFile in additionalConfigurationFiles)
                {
                    configBuilder.AddJsonFile(configFile, true);
                }
            }

            configBuilder.AddEnvironmentVariables();

            if (args != null)
            {
                configBuilder.AddCommandLine(args);
            }

            return configBuilder.Build();
        }
    }
}
