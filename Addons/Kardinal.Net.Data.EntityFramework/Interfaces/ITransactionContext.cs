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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Data
{
    /// <summary>
    /// Interface de uma transação criada a partir de um contexto de base de dados.
    /// </summary>
    public interface ITransactionContext : IDisposable
    {
        /// <summary>
        /// Id da transação.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        DbDataReader Execute(string sqlCommand, CommandBehavior behavior, params KeyValuePair<string, object>[] parameters);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        DbDataReader Execute(string sqlCommand, CommandBehavior behavior, IDictionary<string, object> parameters = null);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns>Contagem de registros afetados.</returns>
        int Execute(string sqlCommand, params KeyValuePair<string, object>[] parameters);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <returns>Contagem de registros afetados.</returns>
        int Execute(string sqlCommand, IDictionary<string, object> parameters = null);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        Task<DbDataReader> ExecuteAsync(string sqlCommand, CommandBehavior behavior, CancellationToken cancellationToken = default, params KeyValuePair<string, object>[] parameters);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="behavior">Descrição do efeito do comando SQL na base de dados.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns><see cref="DbDataReader"/> com o resultado da execução do comando SQL.</returns>
        Task<DbDataReader> ExecuteAsync(string sqlCommand, CommandBehavior behavior, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros afetados.</returns>
        Task<int> ExecuteAsync(string sqlCommand, CancellationToken cancellationToken = default, params KeyValuePair<string, object>[] parameters);

        /// <summary>
        /// Método para execução de uma instrução SQL.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL à ser executado.</param>
        /// <param name="parameters">Parâmetros do comando SQL.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros afetados.</returns>
        Task<int> ExecuteAsync(string sqlCommand, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cria um ponto de salvamento na transação. Isso permite que todos os comandos executados 
        /// após o ponto de salvamento sejam revertidos, restaurando o estado da transação para o 
        /// que estava no momento do ponto de salvamento.
        /// </summary>
        /// <param name="name">O nome do ponto de salvamento a ser criado.</param>
        void CreateSavepoint([NotNull] string name);

        /// <summary>
        /// Cria um ponto de salvamento na transação. Isso permite que todos os comandos executados 
        /// após o ponto de salvamento sejam revertidos, restaurando o estado da transação para o 
        /// que estava no momento do ponto de salvamento.
        /// </summary>
        /// <param name="name">O nome do ponto de salvamento a ser criado.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        Task CreateSavepointAsync([NotNull] string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Reverte todos os comandos que foram executados após o estabelecimento do ponto de salvamento especificado.
        /// </summary>
        /// <param name="name">O nome do ponto de salvamento a ser revertido.</param>
        void RollbackToSavepoint([NotNull] string name);

        /// <summary>
        /// Reverte todos os comandos que foram executados após o estabelecimento do ponto de salvamento especificado.
        /// </summary>
        /// <param name="name">O nome do ponto de salvamento a ser revertido.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        Task RollbackToSavepointAsync([NotNull] string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Destrói um ponto de salvamento previamente definido na transação atual. 
        /// Isso permite que o sistema recupere alguns recursos antes que a transação termine.
        /// </summary>
        /// <param name="name">O nome do ponto de salvamento a ser destruído.</param>
        void ReleaseSavepointAsync([NotNull] string name);

        /// <summary>
        /// Destrói um ponto de salvamento previamente definido na transação atual. 
        /// Isso permite que o sistema recupere alguns recursos antes que a transação termine.
        /// </summary>
        /// <param name="name">O nome do ponto de salvamento a ser destruído.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        Task ReleaseSavepoint([NotNull] string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Confirma todas as alterações feitas na base de dados da transação.
        /// </summary>
        void Commit();

        /// <summary>
        /// Confirma todas as alterações feitas na base de dados da transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Descarta todas as alterações feitas na base de dados da transação.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Descarta todas as alterações feitas na base de dados da transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
