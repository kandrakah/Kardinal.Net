/*
Kardinal.Net
Copyright(C) 2022 Marcelo O.Mendes


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

using Microsoft.EntityFrameworkCore;

namespace Kardinal.Net.Data
{
    /// <summary>
    /// Implementação padrão de um repositório.
    /// </summary>
    /// <typeparam name="TEntity">Entidade gerenciada pelo repositório.</typeparam>
    public sealed class DefaultRepository<TEntity> : AbstractRepository<DbContext, TEntity>, IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context">Instância do contexto do repositório.</param>
        public DefaultRepository(DbContext context) : base(context)
        {

        }

        /// <summary>
        /// Método que efetua a liberação de resursos de um objeto.
        /// </summary>
        public override void Dispose()
        {
        }
    }

    /// <summary>
    /// Implementação padrão de um repositório.
    /// </summary>
    /// <typeparam name="TContext">Contexto provedor do repositório.</typeparam>
    /// <typeparam name="TEntity">Entidade gerenciada pelo repositório.</typeparam>
    public sealed class DefaultRepository<TContext, TEntity> : AbstractRepository<TContext, TEntity>, IRepository<TContext, TEntity> where TContext : DbContext where TEntity : Entity
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context">Instância do contexto do repositório.</param>
        public DefaultRepository(TContext context) : base(context)
        {
        }

        /// <summary>
        /// Método que efetua a liberação de resursos de um objeto.
        /// </summary>
        public override void Dispose()
        {
        }
    }
}
