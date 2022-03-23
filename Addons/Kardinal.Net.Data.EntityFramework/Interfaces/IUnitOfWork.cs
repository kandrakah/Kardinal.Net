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
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Data
{

    /// <summary>
    /// Interface da unidade de trabalho.
    /// </summary>
    /// <typeparam name="TContext">Contexto à ser utilizado pela unidade de trabalho.</typeparam>
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {

    }

    /// <summary>
    /// Interface da unidade de trabalho.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Método que cria uma instância de um <see cref="DbSet{TEntity}"/> de uma entidade.
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
        /// <returns>DbSet da entidade solicitada.</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : Entity;

        /// <summary>
        /// Método para criar um contexto de transação.
        /// </summary>
        /// <returns>Instância do contexto de transação.</returns>
        ITransactionContext CreateTransaction();

        /// <summary>
        /// Método para criar um contexto de transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Instância do contexto de transação.</returns>
        Task<ITransactionContext> CreateTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        DbDataReader ExecuteCommand(string sqlCommand, CommandBehavior behavior, params KeyValuePair<string, object>[] parameters);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        DbDataReader ExecuteCommand(string sqlCommand, CommandBehavior behavior, IDictionary<string, object> parameters = null);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns>Contagem de registros afetados.</returns>
        int ExecuteCommand(string sqlCommand, params KeyValuePair<string, object>[] parameters);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns>Contagem de registros afetados.</returns>
        int ExecuteCommand(string sqlCommand, IDictionary<string, object> parameters = null);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        Task<DbDataReader> ExecuteCommandAsync(string sqlCommand, CommandBehavior behavior, CancellationToken cancellationToken = default, params KeyValuePair<string, object>[] parameters);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        Task<DbDataReader> ExecuteCommandAsync(string sqlCommand, CommandBehavior behavior, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros afetados.</returns>
        Task<int> ExecuteCommandAsync(string sqlCommand, CancellationToken cancellationToken = default, params KeyValuePair<string, object>[] parameters);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros afetados.</returns>
        Task<int> ExecuteCommandAsync(string sqlCommand, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <returns>Contagem de registros alterados.</returns>
        int SaveChanges();

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indica se <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges"/> 
        /// é chamado após as alterações terem sido enviadas com sucesso para o banco de dados.</param>
        /// <returns>Contagem de registros alterados.</returns>
        int SaveChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros alterados.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indica se <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges"/> 
        /// é chamado após as alterações terem sido enviadas com sucesso para o banco de dados.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros alterados.</returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
