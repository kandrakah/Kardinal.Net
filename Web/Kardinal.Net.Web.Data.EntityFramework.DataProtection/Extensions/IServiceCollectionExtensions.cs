using Kardinal.Net.Data;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;

namespace Kardinal.Net.Web.Data
{
    /// <summary>
    /// Extensões para <see cref="IServiceCollection"/>
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Extensão para adição do serviço de proteção de dados.
        /// </summary>
        /// <typeparam name="TContext" >Contexto para persistência das chaves de segurança.</typeparam>
        /// <param name="services">Objeto referenciado.</param>
        /// <param name="configuration">Instância do contâiner de configurações.</param>
        /// <param name="sector">Setor onde se encontram as configurações de proteção de dados.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddDataProtection<TContext>(this IServiceCollection services, IConfiguration configuration, string sector = "DataProtection") where TContext : DbContext, IDataProtectionKeyContext
        {
            var config = configuration.GetOptions<DataProtectionOptions>(sector);
            services.AddDataProtection<TContext>(config);
            return services;
        }

        /// <summary>
        /// Extensão para adição do serviço de proteção de dados.
        /// </summary>
        /// <typeparam name="TContext" >Contexto para persistência das chaves de segurança.</typeparam>
        /// <param name="services">Objeto referenciado.</param>
        /// <param name="configuration">Configurações do serviço de proteção de dados.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddDataProtection<TContext>(this IServiceCollection services, DataProtectionOptions configuration) where TContext : DbContext, IDataProtectionKeyContext
        {
            var appName = string.IsNullOrEmpty(configuration.ApplicationName) ? Constants.ApplicationName : configuration.ApplicationName;

            var builder = services.AddDataProtection()
                .SetApplicationName(appName)
                .PersistKeysToDbContext<TContext>();

            switch (configuration.Source)
            {
                case DataProtectionCertificateSourceType.FileSystem:
                    AddCertificateByFileSytem(builder, configuration.Path, configuration.Password);
                    break;
                case DataProtectionCertificateSourceType.Thumbprint:
                    AddCertificateByThumbprint(builder, configuration.StoreName, configuration.StoreLocation, configuration.ValidOnly, configuration.Thumbprint);
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
                builder.ProtectKeysWithCertificate(new X509Certificate2(path, password, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet));
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

                    var cert = certs.First();

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
