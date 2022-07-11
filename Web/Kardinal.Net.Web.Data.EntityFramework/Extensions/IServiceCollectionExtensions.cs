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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Extensões para <see cref="IServiceCollection"/>
    /// </summary>
    public static class IServiceCollectionExtensions
    {
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
