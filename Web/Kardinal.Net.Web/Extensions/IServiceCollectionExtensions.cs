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

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de extensões para <see cref="IServiceCollection"/>.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Extensão para adição de serviço de informações de sistema.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddApplicationInfoService(this IServiceCollection services)
        {
            services.AddSingleton<IApplicationInfoService, ApplicationInfoService>();
            return services;
        }

        /// <summary>
        /// Extensão para adição do usuário de sistema.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddBackgroundTaskHostedService(this IServiceCollection services)
        {
            return services.AddBackgroundTaskHostedService(o =>
            {
                o.BackgrounTaskQueueCapacity = 1000;
                o.BackgroundTaskFullMode = System.Threading.Channels.BoundedChannelFullMode.Wait;
            });
        }

        /// <summary>
        /// Extensão para adição do usuário de sistema.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <param name="options">Configurações do serviço de fila.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddBackgroundTaskHostedService(this IServiceCollection services, Action<BackgroundTaskServiceOptions> options)
        {
            services.Configure(options);
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddHostedService<BackgroundTaskHostedService>();
            return services;
        }

        /// <summary>
        /// Extensão para adição do serviço de proteção de dados.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <param name="configuration">Instância do contâiner de configurações.</param>
        /// <param name="sector">Setor onde se encontram as configurações de proteção de dados.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddDataProtection(this IServiceCollection services, IConfiguration configuration, string sector = "DataProtection")
        {
            var config = configuration.GetOptions<DataProtectionOptions>(sector);
            services.AddDataProtection(config);
            return services;
        }

        /// <summary>
        /// Extensão para adição do serviço de proteção de dados.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <param name="options">Configurações do serviço de proteção de dados.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddDataProtection(this IServiceCollection services, DataProtectionOptions options)
        {
            var appName = string.IsNullOrEmpty(options.ApplicationName) ? Constants.ApplicationName : options.ApplicationName;

            var builder = services.AddDataProtection()
                .SetApplicationName(appName);

            switch (options.KeyStorage)
            {
                case DataProtectionKeyStorageType.FileSystem:
                    if (string.IsNullOrEmpty(options.Path))
                    {
                        throw new KardinalException("O diretório de armazenamento das chaves de segurança é requerido.");
                    }
                    var directory = new DirectoryInfo(options.Path);
                    builder.PersistKeysToFileSystem(directory);
                    break;
            }

            switch (options.Source)
            {
                case DataProtectionCertificateSourceType.FileSystem:
                    AddCertificateByFileSytem(builder, options.Path, options.Password);
                    break;
                case DataProtectionCertificateSourceType.Thumbprint:
                    AddCertificateByThumbprint(builder, options.StoreName, options.StoreLocation, options.ValidOnly, options.Thumbprint);
                    break;
                default:
                    break;
            }

            return services;
        }

        /// <summary>
        /// Método para carregamento do certificado de proteção de dados através do sistema de arquivos.
        /// </summary>
        /// <param name="builder">Instância do construtor de proteção de dados.</param>
        /// <param name="path">Caminho do certificado no sistema de arquivos.</param>
        /// <param name="password">Senha do certificado.</param>
        private static void AddCertificateByFileSytem(IDataProtectionBuilder builder, string path, string password)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("O diretório do certificado não pode ser nulo ou vazio!");
            }

            if (!File.Exists(path))
            {
                throw new ArgumentException("O arquivo do certificado informado não foi localizado!");
            }

            try
            {
                builder.ProtectKeysWithCertificate(new X509Certificate2(path, password, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet));
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Falha ao carregar certificado para proteção de dados: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Método para carregamento do certificado de proteção de dados através repositório de certificados.
        /// </summary>
        /// <param name="builder">Instância do construtor de proteção de dados.</param>
        /// <param name="storeName">Nome do diretório de certificados.</param>
        /// <param name="storeLocation">Localização do diretório de certificados.</param>
        /// <param name="validOnly">Indica que apenas certificados válidos serão buscados.</param>
        /// <param name="thumbprint">Impressão digital do certificado.</param>
        private static void AddCertificateByThumbprint(IDataProtectionBuilder builder, StoreName storeName, StoreLocation storeLocation, bool validOnly, string thumbprint)
        {
            if (string.IsNullOrEmpty(thumbprint))
            {
                throw new ArgumentException("A impressão digital do certificado é nula ou vazia!");
            }

            try
            {
                using (var store = new X509Store(storeName, storeLocation, OpenFlags.ReadOnly))
                {
                    var certs = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, validOnly).Cast<X509Certificate2>();
                    if (certs == null || certs.Any())
                    {
                        throw new ArgumentException($"Certificado com impressão digital [{thumbprint}] não localizado no diretório [{storeName}] em [{storeLocation}]");
                    }

                    var cert = certs.FirstOrDefault();

                    builder.ProtectKeysWithCertificate(cert);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Falha ao carregar certificado para proteção de dados: {ex.Message}", ex);
            }
        }
    }
}
