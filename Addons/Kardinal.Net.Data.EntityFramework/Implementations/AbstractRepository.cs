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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Data
{
    /// <summary>
    /// Classe abstrata de repositório de dados.
    /// </summary>
    /// <typeparam name="TContext">Tipo do contexto.</typeparam>
    /// <typeparam name="TEntity">Tipo da entidade gerenciada.</typeparam>
    public abstract class AbstractRepository<TContext, TEntity> where TContext : DbContext where TEntity : Entity
    {
        /// <summary>
        /// Instância do contexto do repositório.
        /// </summary>
        protected readonly TContext _context;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context">Instância do contexto do repositório.</param>
        public AbstractRepository(TContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> AsQueryable()
        {
            return this._context.Set<TEntity>().AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAsyncEnumerable<TEntity> AsAsyncEnumerable()
        {
            return this._context.Set<TEntity>().AsAsyncEnumerable();
        }

        /// <summary>
        /// Método que faz a busca de um registro de uma entidade
        /// com base em suas chaves primárias informadas.
        /// </summary>
        /// <param name="keyValues">Chaves primárias da entidade.</param>
        /// <returns>Dados do registro associado às chaves primárias informadas.
        /// Caso nenhum registro seja encontrado, retornará nulo.</returns>
        public TEntity Find(params object[] keyValues)
        {
            return this._context.Set<TEntity>().Find(keyValues);
        }

        /// <summary>
        /// Método que faz a busca de um registro de uma entidade
        /// com base em suas chaves primárias informadas.
        /// </summary>
        /// <param name="keyValues">Chaves primárias da entidade.</param>
        /// <returns>Dados do registro associado às chaves primárias informadas.
        /// Caso nenhum registro seja encontrado, retornará nulo.</returns>
        public ValueTask<TEntity> FindAsync(params object[] keyValues)
        {
            return this._context.Set<TEntity>().FindAsync(keyValues);
        }

        /// <summary>
        /// Método que faz a busca de um registro de uma entidade
        /// com base em suas chaves primárias informadas.
        /// </summary>
        /// <param name="keyValues">Chaves primárias da entidade.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados do registro associado às chaves primárias informadas.
        /// Caso nenhum registro seja encontrado, retornará nulo.</returns>
        public ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            return this._context.Set<TEntity>().FindAsync(keyValues, cancellationToken);
        }

        /// <summary>
        /// Método para a adição de um novo registro no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entity">Entidade à ser adicionada.</param>
        public void Add(TEntity entity)
        {
            this._context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Método para a adição de um novo registro no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entity">Entidade à ser adicionada.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await this._context.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Método para a adição de vários novos registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            this._context.Set<TEntity>().AddRange(entities);
        }

        /// <summary>
        /// Método para a adição de vários novos registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        public void AddRange(params TEntity[] entities)
        {
            this._context.Set<TEntity>().AddRange(entities);
        }

        /// <summary>
        /// Método para a adição de vários novos registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return this._context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// Método para a adição de vários novos registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <param name="entities">Entidades à serem adicionadas.</param>        
        public Task AddRangeAsync(CancellationToken cancellationToken = default, params TEntity[] entities)
        {
            return this._context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// Começa a rastrear uma entidade fornecida e as entradas acessíveis a partir dessa entidade
        /// usando o estado <see cref="EntityState.Unchanged"/> por padrão.
        /// </summary>
        /// <param name="entity">entidade à ser rastreada.</param>
        public void Attach(TEntity entity)
        {
            this._context.Set<TEntity>().Attach(entity);
        }

        /// <summary>
        /// Começa a rastrear várias entidades fornecidas e as entradas acessíveis a partir dessa entidade
        /// usando o estado <see cref="EntityState.Unchanged"/> por padrão.
        /// </summary>
        /// <param name="entity">entidade à ser rastreada.</param>
        public void AttachRange(IEnumerable<TEntity> entity)
        {
            this._context.Set<TEntity>().AttachRange(entity);
        }

        /// <summary>
        /// Começa a rastrear várias entidades fornecidas e as entradas acessíveis a partir dessa entidade
        /// usando o estado <see cref="EntityState.Unchanged"/> por padrão.
        /// </summary>
        /// <param name="entity">entidade à ser rastreada.</param>
        public void AttachRange(TEntity[] entity)
        {
            this._context.Set<TEntity>().AttachRange(entity);
        }

        /// <summary>
        /// Método para a edição de um registro no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entity">Entidade à ser modificada.</param>
        public void Update(TEntity entity)
        {
            this._context.Set<TEntity>().Update(entity);
        }

        /// <summary>
        /// Método para a edição de vários registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entities">Entidades à serem modificadas.</param>
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            this._context.Set<TEntity>().UpdateRange(entities);
        }

        /// <summary>
        /// Método para a edição de vários registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entities">Entidades à serem modificadas.</param>
        public void UpdateRange(params TEntity[] entities)
        {
            this._context.Set<TEntity>().UpdateRange(entities);
        }

        /// <summary>
        /// Método para a remoção de um registro no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entity">Entidade à ser removida.</param>
        public void Remove(TEntity entity)
        {
            this._context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Método para a remoção de vários registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entity">Entidades à serem removida.</param>
        public void RemoveRange(IEnumerable<TEntity> entity)
        {
            this._context.Set<TEntity>().RemoveRange(entity);
        }

        /// <summary>
        /// Método para a remoção de vários registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entity">Entidades à serem removida.</param>
        public void RemoveRange(params TEntity[] entity)
        {
            this._context.Set<TEntity>().RemoveRange(entity);
        }

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <returns>Contagem de registros alterados.</returns>
        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indica se <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges"/> 
        /// é chamado após as alterações terem sido enviadas com sucesso para o banco de dados.</param>
        /// <returns>Contagem de registros alterados.</returns>
        public void SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this._context.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros alterados.</returns>
        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this._context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indica se <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges"/> 
        /// é chamado após as alterações terem sido enviadas com sucesso para o banco de dados.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros alterados.</returns>
        public Task SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return this._context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Método que efetua a liberação de resursos de um objeto.
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}

