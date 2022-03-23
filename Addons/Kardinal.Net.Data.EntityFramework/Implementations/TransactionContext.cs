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
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Data
{
    /// <summary>
    /// Implementação de uma transação criada a partir de um contexto de base de dados.
    /// </summary>
    public sealed class TransactionContext : ITransactionContext
    {
        /// <summary>
        /// Id da transação.
        /// </summary>
        public Guid Id => this.ContextTransaction != null ? this.ContextTransaction.TransactionId : Guid.Empty;

        /// <summary>
        /// Instância do contexto de dados.
        /// </summary>
        private readonly DbContext _context;

        /// <summary>
        /// Instância da transação atual.
        /// </summary>
        private IDbContextTransaction ContextTransaction { get; set; }

        /// <summary>
        /// Enumeração de readers da transação.
        /// </summary>
        private readonly ConcurrentBag<DbDataReader> _readers;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context">Instância do contexto de dados.</param>
        internal TransactionContext([NotNull] DbContext context)
        {
            this._context = context;
            this._readers = new ConcurrentBag<DbDataReader>();
        }

        /// <summary>
        /// Método que cria uma nova transação.
        /// </summary>
        internal void BeginTransaction()
        {
            if (this.ContextTransaction == null)
            {
                this.ContextTransaction = this._context.Database.BeginTransaction();
            }
        }

        /// <summary>
        /// Método que cria uma nova transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        internal async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (this.ContextTransaction == null)
            {
                this.ContextTransaction = await this._context.Database.BeginTransactionAsync(cancellationToken);
            }
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        public DbDataReader Execute(string sqlCommand, CommandBehavior behavior, params KeyValuePair<string, object>[] parameters)
        {
            var p = new Dictionary<string, object>(parameters.ToList());
            return this.Execute(sqlCommand, behavior, p);
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        public DbDataReader Execute(string sqlCommand, CommandBehavior behavior, IDictionary<string, object> parameters = null)
        {
            var connection = this._context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            using (var cmd = connection.CreateCommand())
            {
                cmd.Transaction = this.ContextTransaction.GetDbTransaction();
                cmd.CommandText = sqlCommand;
                if (parameters != null && parameters.Any())
                {
                    foreach (var p in parameters)
                    {
                        cmd.AddParameter(p.Key, p.Value);
                    }
                }

                var reader = cmd.ExecuteReader(behavior);
                this._readers.Add(reader);
                return reader;
            }
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns>Contagem de registros afetados.</returns>
        public int Execute(string sqlCommand, params KeyValuePair<string, object>[] parameters)
        {
            var p = new Dictionary<string, object>(parameters.ToList());
            return this.Execute(sqlCommand, p);
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns>Contagem de registros afetados.</returns>
        public int Execute(string sqlCommand, IDictionary<string, object> parameters = null)
        {
            var connection = this._context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            var cmd = connection.CreateCommand();
            cmd.Transaction = this.ContextTransaction.GetDbTransaction();
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
        public Task<DbDataReader> ExecuteAsync(string sqlCommand, CommandBehavior behavior, CancellationToken cancellationToken = default, params KeyValuePair<string, object>[] parameters)
        {
            var p = new Dictionary<string, object>(parameters.ToList());
            return this.ExecuteAsync(sqlCommand, behavior, p, cancellationToken);
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        public async Task<DbDataReader> ExecuteAsync(string sqlCommand, CommandBehavior behavior, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var connection = this._context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync(cancellationToken);
            }
            using (var cmd = connection.CreateCommand())
            {
                cmd.Transaction = this.ContextTransaction.GetDbTransaction();
                cmd.CommandText = sqlCommand;
                if (parameters != null && parameters.Any())
                {
                    foreach (var p in parameters)
                    {
                        cmd.AddParameter(p.Key, p.Value);
                    }
                }

                var reader = await cmd.ExecuteReaderAsync(behavior, cancellationToken);
                this._readers.Add(reader);
                return reader;
            }
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros afetados.</returns>
        public Task<int> ExecuteAsync(string sqlCommand, CancellationToken cancellationToken = default, params KeyValuePair<string, object>[] parameters)
        {
            var p = new Dictionary<string, object>(parameters.ToList());
            return this.ExecuteAsync(sqlCommand, p, cancellationToken);
        }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros afetados.</returns>
        public async Task<int> ExecuteAsync(string sqlCommand, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var connection = this._context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync(cancellationToken);
            }
            var cmd = connection.CreateCommand();
            cmd.Transaction = this.ContextTransaction.GetDbTransaction();
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
        /// Cria um ponto de salvamento na transação. Isso permite que todos os comandos executados 
        /// após o ponto de salvamento sejam revertidos, restaurando o estado da transação para o 
        /// que estava no momento do ponto de salvamento.
        /// </summary>
        /// <param name="name">O nome do ponto de salvamento a ser criado.</param>
        public void CreateSavepoint([NotNull] string name)
        {
            this.ContextTransaction.CreateSavepoint(name);
        }

        /// <summary>
        /// Cria um ponto de salvamento na transação. Isso permite que todos os comandos executados 
        /// após o ponto de salvamento sejam revertidos, restaurando o estado da transação para o 
        /// que estava no momento do ponto de salvamento.
        /// </summary>
        /// <param name="name">O nome do ponto de salvamento a ser criado.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        public Task CreateSavepointAsync([NotNull] string name, CancellationToken cancellationToken = default)
        {
            return this.ContextTransaction.CreateSavepointAsync(name, cancellationToken);
        }

        /// <summary>
        /// Reverte todos os comandos que foram executados após o estabelecimento do ponto de salvamento especificado.
        /// </summary>
        /// <param name="name">O nome do ponto de salvamento a ser revertido.</param>
        public void RollbackToSavepoint([NotNull] string name)
        {
            this.ContextTransaction.RollbackToSavepoint(name);
        }

        /// <summary>
        /// Reverte todos os comandos que foram executados após o estabelecimento do ponto de salvamento especificado.
        /// </summary>
        /// <param name="name">O nome do ponto de salvamento a ser revertido.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        public Task RollbackToSavepointAsync([NotNull] string name, CancellationToken cancellationToken = default)
        {
            return this.ContextTransaction.RollbackToSavepointAsync(name, cancellationToken);
        }

        /// <summary>
        /// Destrói um ponto de salvamento previamente definido na transação atual. 
        /// Isso permite que o sistema recupere alguns recursos antes que a transação termine.
        /// </summary>
        /// <param name="name">O nome do ponto de salvamento a ser destruído.</param>
        public void ReleaseSavepointAsync([NotNull] string name)
        {
            this.ContextTransaction.ReleaseSavepointAsync(name);
        }

        /// <summary>
        /// Destrói um ponto de salvamento previamente definido na transação atual. 
        /// Isso permite que o sistema recupere alguns recursos antes que a transação termine.
        /// </summary>
        /// <param name="name">O nome do ponto de salvamento a ser destruído.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        public Task ReleaseSavepoint([NotNull] string name, CancellationToken cancellationToken = default)
        {
            return this.ContextTransaction.ReleaseSavepointAsync(name, cancellationToken);
        }

        /// <summary>
        /// Confirma todas as alterações feitas na base de dados da transação.
        /// </summary>
        public void Commit()
        {
            this.ContextTransaction.Commit();
        }

        /// <summary>
        /// Confirma todas as alterações feitas na base de dados da transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return this.ContextTransaction.CommitAsync(cancellationToken);
        }

        /// <summary>
        /// Descarta todas as alterações feitas na base de dados da transação.
        /// </summary>
        public void Rollback()
        {
            this.ContextTransaction.Rollback();
        }

        /// <summary>
        /// Descarta todas as alterações feitas na base de dados da transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            return this.ContextTransaction.RollbackAsync(cancellationToken);
        }

        /// <summary>
        /// Executa tarefas definidas pelo aplicativo associadas à liberação ou redefinição de recursos não gerenciados.
        /// </summary>
        public void Dispose()
        {
            if (this._readers.Any())
            {
                foreach (var reader in this._readers)
                {
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                    reader.DisposeAsync().AsTask().Wait();
                }
            }

            this.ContextTransaction?.Dispose();
        }

        /// <summary>
        /// Executa tarefas definidas pelo aplicativo associadas à liberação ou redefinição de recursos não gerenciados.
        /// </summary>
        /// <returns></returns>
        public async ValueTask DisposeAsync()
        {
            if (this._readers.Any())
            {
                foreach (var reader in this._readers)
                {
                    if (!reader.IsClosed)
                    {
                        await reader.CloseAsync();
                    }
                    await reader.DisposeAsync();
                }
            }

            await this.ContextTransaction.DisposeAsync();
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
