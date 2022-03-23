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

using Kardinal.Net.Data;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Kardinal.Net.Web
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

                    var cert = certs.FirstOrDefault();

                    builder.ProtectKeysWithCertificate(cert);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Falha ao carregar certificado para proteção de dados: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Extensão para a adição do serviço de unidade de trabalho responsável pela
        /// gerência de entidades junto ao Entity Framework.
        /// 
        /// Esta opção aplica-se à sistemas com múltiplos contextos, sendo necessário
        /// seu acesso em casos específicos.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            return services;
        }

        /// <summary>
        /// Extensão para a adição do serviço de unidade de trabalho responsável pela
        /// gerência de entidades junto ao Entity Framework.
        /// 
        /// Esta opção se aplica a definição de um contexto específico como padrão 
        /// para a utilização.
        /// </summary>
        /// <typeparam name="TContext">Tipo de contexto à ser atribuído como padrão.</typeparam>
        /// <param name="services">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddScoped<TContext, TContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            return services;
        }

        /// <summary>
        /// Extensão para adição de repositórios de entidades
        /// do Entity Framework.
        /// 
        /// Esta opção destina-se à adicionar um serviço genérico para a utilização
        /// com qualquer entidade registrada no contexto padrão.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(DefaultRepository<>));
            services.AddScoped(typeof(IRepository<,>), typeof(DefaultRepository<,>));
            return services;
        }

        /// <summary>
        /// Extensão para adição de repositório de uma entidade do Entity Framework.
        /// 
        /// Esta opção destina-se à adicionar um repositório para a entidade informada.
        /// É possível adicionar um repositório para cada entidade existente no contexto padrão.
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade gerenciada pelo contexto.</typeparam>
        /// <param name="services">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddRepository<TEntity>(this IServiceCollection services) where TEntity : Entity
        {
            services.AddScoped<IRepository<TEntity>, DefaultRepository<TEntity>>();
            return services;
        }

        /// <summary>
        /// Extensão para adição de repositório de uma entidade do Entity Framework.
        /// 
        /// Esta opção destina-se à adicionar um repositório para a entidade informada.
        /// É possível adicionar um repositório para cada entidade existente no contexto <typeparamref name="TContext"/>.
        /// </summary>
        /// <typeparam name="TContext">Tipo do contexto o qual a entidade está associada.</typeparam>
        /// <typeparam name="TEntity">Tipo da entidade gerenciada pelo contexto.</typeparam>
        /// <param name="services">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddRepository<TContext, TEntity>(this IServiceCollection services) where TContext : DbContext where TEntity : Entity
        {
            services.AddScoped<IRepository<TContext, TEntity>, DefaultRepository<TContext, TEntity>>();
            return services;
        }
    }
}
