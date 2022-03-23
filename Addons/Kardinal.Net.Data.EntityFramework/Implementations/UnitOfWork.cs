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
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Data
{
    /// <summary>
    /// Implementação da unidade de trabalho.
    /// </summary>
    /// <typeparam name="TContext">Contexto à ser utilizado pela unidade de trabalho.</typeparam>
    public sealed class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Instância do serviço de log.
        /// </summary>
        private readonly ILogger<UnitOfWork<TContext>> _logger;

        /// <summary>
        /// Instância do contexto.
        /// </summary>
        private readonly TContext _context;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="logger">Instância do serviço de log.</param>
        /// <param name="context">Instância do contexto.</param>
        public UnitOfWork(ILogger<UnitOfWork<TContext>> logger, TContext context)
        {
            this._logger = logger;
            this._context = context;
        }

        /// <summary>
        /// Método que cria uma instância de um <see cref="DbSet{TEntity}"/> de uma entidade.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade.</typeparam>
        /// <returns>DbSet da entidade solicitada.</returns>
        public DbSet<T> Set<T>() where T : Entity
        {
            return this._context.Set<T>();
        }

        /// <summary>
        /// Método para criar um contexto de transação.
        /// </summary>
        /// <returns>Instância do contexto de transação.</returns>
        public ITransactionContext CreateTransaction()
        {
            var transaction = new TransactionContext(this._context);
            transaction.BeginTransaction();
            return transaction;
        }

        /// <summary>
        /// Método para criar um contexto de transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Instância do contexto de transação.</returns>
        public async Task<ITransactionContext> CreateTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = new TransactionContext(this._context);
            await transaction.BeginTransactionAsync(cancellationToken);
            return transaction;
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        public DbDataReader ExecuteCommand(string sqlCommand, CommandBehavior behavior, params KeyValuePair<string, object>[] parameters)
        {
            var p = new Dictionary<string, object>(parameters.ToList());
            return this.ExecuteCommand(sqlCommand, behavior, p);
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        public DbDataReader ExecuteCommand(string sqlCommand, CommandBehavior behavior, IDictionary<string, object> parameters = null)
        {
            var connection = this._context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            var cmd = connection.CreateCommand();
            cmd.CommandText = sqlCommand;
            if (parameters != null && parameters.Any())
            {
                foreach (var p in parameters)
                {
                    cmd.AddParameter(p.Key, p.Value);
                }
            }

            return cmd.ExecuteReader(behavior);
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns>Contagem de registros afetados.</returns>
        public int ExecuteCommand(string sqlCommand, params KeyValuePair<string, object>[] parameters)
        {
            var p = new Dictionary<string, object>(parameters.ToList());
            return this.ExecuteCommand(sqlCommand, p);
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns>Contagem de registros afetados.</returns>
        public int ExecuteCommand(string sqlCommand, IDictionary<string, object> parameters = null)
        {
            var connection = this._context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            var cmd = connection.CreateCommand();
            cmd.CommandText = sqlCommand;
            if (parameters != null && parameters.Any())
            {
                foreach (var p in parameters)
                {
                    cmd.AddParameter(p.Key, p.Value);
                }
            }

            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        public Task<DbDataReader> ExecuteCommandAsync(string sqlCommand, CommandBehavior behavior, CancellationToken cancellationToken = default, params KeyValuePair<string, object>[] parameters)
        {
            var p = new Dictionary<string, object>(parameters.ToList());
            return this.ExecuteCommandAsync(sqlCommand, behavior, p, cancellationToken);
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        public async Task<DbDataReader> ExecuteCommandAsync(string sqlCommand, CommandBehavior behavior, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var connection = this._context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync(cancellationToken);
            }
            var cmd = connection.CreateCommand();
            cmd.CommandText = sqlCommand;
            if (parameters != null && parameters.Any())
            {
                foreach (var p in parameters)
                {
                    cmd.AddParameter(p.Key, p.Value);
                }
            }

            return await cmd.ExecuteReaderAsync(behavior, cancellationToken);
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros afetados.</returns>
        public Task<int> ExecuteCommandAsync(string sqlCommand, CancellationToken cancellationToken = default, params KeyValuePair<string, object>[] parameters)
        {
            var p = new Dictionary<string, object>(parameters.ToList());
            return this.ExecuteCommandAsync(sqlCommand, p, cancellationToken);
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros afetados.</returns>
        public async Task<int> ExecuteCommandAsync(string sqlCommand, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var connection = this._context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync(cancellationToken);
            }
            var cmd = connection.CreateCommand();
            cmd.CommandText = sqlCommand;
            if (parameters != null && parameters.Any())
            {
                foreach (var p in parameters)
                {
                    cmd.AddParameter(p.Key, p.Value);
                }
            }

            return await cmd.ExecuteNonQueryAsync(cancellationToken);
        }

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <returns>Contagem de registros alterados.</returns>
        public int SaveChanges()
        {
            var result = this._context.SaveChanges();
            return result;
        }

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indica se <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges"/> 
        /// é chamado após as alterações terem sido enviadas com sucesso para o banco de dados.</param>
        /// <returns>Contagem de registros alterados.</returns>
        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var result = this._context.SaveChanges(acceptAllChangesOnSuccess);
            return result;
        }

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros alterados.</returns>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await this._context.SaveChangesAsync(cancellationToken);
            return result;
        }

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indica se <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges"/> 
        /// é chamado após as alterações terem sido enviadas com sucesso para o banco de dados.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros alterados.</returns>
        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var result = await this._context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return result;
        }

        /// <summary>
        /// Método que efetua a liberação de resursos de um objeto.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
