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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Data
{
    /// <summary>
    /// Interface do repositório do Entity Framework.
    /// </summary>
    /// <typeparam name="TContext">Tipo do contexto do repositório.</typeparam>
    /// <typeparam name="TEntity">Entidade associada ao repositório.</typeparam>
    public interface IRepository<TContext, TEntity> : IRepository<TEntity> where TContext : DbContext where TEntity : Entity
    {

    }

    /// <summary>
    /// Interface do repositório do Entity Framework.
    /// </summary>
    /// <typeparam name="TEntity">Entidade associada ao repositório.</typeparam>
    public interface IRepository<TEntity> : IRepository where TEntity : Entity
    {
        /// <summary>
        /// Método que obtém uma instância de <see cref="IQueryable{TEntity}"/> para a entidade
        /// gerenciada pelo repositório.
        /// </summary>
        /// <returns>Instância de <see cref="IQueryable{TEntity}"/> da entidade gerenciada.</returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Método que obtém uma instância de <see cref="IAsyncEnumerable{TEntity}"/> para a entidade
        /// gerenciada pelo repositório.
        /// </summary>
        /// <returns>Instância de <see cref="IAsyncEnumerable{TEntity}"/> da entidade gerenciada.</returns>
        IAsyncEnumerable<TEntity> AsAsyncEnumerable();

        /// <summary>
        /// Método que faz a busca de um registro de uma entidade
        /// com base em suas chaves primárias informadas.
        /// </summary>
        /// <param name="keyValues">Chaves primárias da entidade.</param>
        /// <returns>Dados do registro associado às chaves primárias informadas.
        /// Caso nenhum registro seja encontrado, retornará nulo.</returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// Método que faz a busca de um registro de uma entidade
        /// com base em suas chaves primárias informadas.
        /// </summary>
        /// <param name="keyValues">Chaves primárias da entidade.</param>
        /// <returns>Dados do registro associado às chaves primárias informadas.
        /// Caso nenhum registro seja encontrado, retornará nulo.</returns>
        ValueTask<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// Método para a adição de um novo registro no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entity">Entidade à ser adicionada.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Método para a adição de um novo registro no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entity">Entidade à ser adicionada.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para a adição de vários novos registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        public void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Método para a adição de vários novos registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        public void AddRange(params TEntity[] entities);

        /// <summary>
        /// Método para a adição de vários novos registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para a adição de vários novos registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <param name="entities">Entidades à serem adicionadas.</param>        
        Task AddRangeAsync(CancellationToken cancellationToken = default, params TEntity[] entities);

        /// <summary>
        /// Começa a rastrear uma entidade fornecida e as entradas acessíveis a partir dessa entidade
        /// usando o estado <see cref="EntityState.Unchanged"/> por padrão.
        /// </summary>
        /// <param name="entity">entidade à ser rastreada.</param>
        void Attach(TEntity entity);

        /// <summary>
        /// Começa a rastrear várias entidades fornecidas e as entradas acessíveis a partir dessa entidade
        /// usando o estado <see cref="EntityState.Unchanged"/> por padrão.
        /// </summary>
        /// <param name="entity">entidade à ser rastreada.</param>
        void AttachRange(IEnumerable<TEntity> entity);

        /// <summary>
        /// Começa a rastrear várias entidades fornecidas e as entradas acessíveis a partir dessa entidade
        /// usando o estado <see cref="EntityState.Unchanged"/> por padrão.
        /// </summary>
        /// <param name="entity">entidade à ser rastreada.</param>
        void AttachRange(TEntity[] entity);

        /// <summary>
        /// Método para a edição de um registro no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entity">Entidade à ser modificada.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Método para a edição de vários registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entities">Entidades à serem modificadas.</param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Método para a edição de vários registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entities">Entidades à serem modificadas.</param>
        void UpdateRange(params TEntity[] entities);

        /// <summary>
        /// Método para a remoção de um registro no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entity">Entidade à ser removida.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Método para a remoção de vários registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entity">Entidades à serem removida.</param>
        void RemoveRange(IEnumerable<TEntity> entity);

        /// <summary>
        /// Método para a remoção de vários registros no repositório
        /// de entidades.
        /// </summary>
        /// <param name="entity">Entidades à serem removida.</param>
        void RemoveRange(params TEntity[] entity);
    }

    /// <summary>
    /// Interface do repositório do Entity Framework.
    /// </summary>
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <returns>Contagem de registros alterados.</returns>
        void SaveChanges();

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indica se <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges"/> 
        /// é chamado após as alterações terem sido enviadas com sucesso para o banco de dados.</param>
        /// <returns>Contagem de registros alterados.</returns>
        void SaveChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros alterados.</returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indica se <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges"/> 
        /// é chamado após as alterações terem sido enviadas com sucesso para o banco de dados.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros alterados.</returns>
        public Task SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
