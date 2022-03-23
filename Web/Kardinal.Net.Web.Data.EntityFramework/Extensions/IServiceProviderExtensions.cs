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
using System;

namespace Kardinal.Net.Web.Data
{
    /// <summary>
    /// Extensões para <see cref="IServiceProvider"/>.
    /// </summary>
    public static class IServiceProviderExtensions
    {
        /// <summary>
        /// Extensão que obtém o serviço de unidade de trabalho.
        /// </summary>
        /// <param name="provider">Objeto referenciado.</param>
        /// <returns>Instância da unidade de trabalho.</returns>
        public static IUnitOfWork<DbContext> GetUnitOfWork(this IServiceProvider provider)
        {
            return provider.GetUnitOfWork<DbContext>();
        }

        /// <summary>
        /// Extensão que obtém o serviço de unidade de trabalho.
        /// </summary>
        /// <typeparam name="TContext">Tipo do contexto à ser aplicado.</typeparam>
        /// <param name="provider">Objeto referenciado.</param>
        /// <returns>Instância da unidade de trabalho.</returns>
        public static IUnitOfWork<TContext> GetUnitOfWork<TContext>(this IServiceProvider provider) where TContext : DbContext
        {
            var unitOfWork = provider.GetKardinalService<IUnitOfWork<TContext>>();
            return unitOfWork;
        }

        /// <summary>
        /// Extensão que obtém uma instância do repositório de uma entidade.
        /// </summary>
        /// <typeparam name="TEntity">Entidade gerenciada pelo repositório.</typeparam>
        /// <param name="provider">Objeto referenciado.</param>
        /// <returns>Instância do repositório da entidade.</returns>
        public static IRepository<TEntity> GetRepository<TEntity>(this IServiceProvider provider) where TEntity : Entity
        {
            return provider.GetKardinalService<IRepository<TEntity>>();
        }

        /// <summary>
        /// Extensão que obtém uma instância do repositório de uma entidade.
        /// </summary>
        /// <typeparam name="TContext">Tipo do contexto do repositório.</typeparam>
        /// <typeparam name="TEntity">Tipo da entidade gerenciada pelo repositório.</typeparam>
        /// <param name="provider">Objeto referenciado.</param>
        /// <returns>Instância do repositório da entidade.</returns>
        public static IRepository<TEntity> GetRepository<TContext, TEntity>(this IServiceProvider provider) where TContext : DbContext where TEntity : Entity
        {
            return provider.GetKardinalService<IRepository<TContext, TEntity>>();
        }
    }
}
