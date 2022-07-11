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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web.Auth
{
    /// <summary>
    /// Interface do repositório de permissões.
    /// </summary>
    public interface IPermissionsStore : IDisposable
    {
        /// <summary>
        /// Método para obtenção da listagem parinada de permissões.
        /// </summary>
        /// <param name="skip">Itens à serem ignorados.</param>
        /// <param name="take">Itens à serem obtidos.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Enumeração paginada de </returns>
        Task<IPaginatedCollection<PermissionModel>> GetPermissionsAsync(int skip, int take, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para obtenção da listagem parinada de permissões.
        /// </summary>
        /// <param name="ids">Ids das permissões à serem buscadas.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Enumeração paginada de </returns>
        Task<IPaginatedCollection<PermissionModel>> GetPermissionsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para obtenção da listagem parinada de permissões.
        /// </summary>
        /// <param name="request">Dados de requisição de listagem de permissões.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Enumeração paginada de </returns>
        Task<IPaginatedCollection<PermissionModel>> GetPermissionsAsync([NotNull] SearchRequestModel request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para obtenção de uma permissão.
        /// </summary>
        /// <param name="id">Id da permissão à ser obtida.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados da permissão.</returns>
        Task<PermissionModel> GetPermissionAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para obtenção de uma permissão.
        /// </summary>
        /// <param name="key">Chave de identificação da permissão.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados da permissão.</returns>
        Task<PermissionModel> GetPermissionAsync([NotNull] string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para adição de uma nova permissão.
        /// </summary>
        /// <param name="permission">Dados da permissão à ser adicionada.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados da permissão adicionada.</returns>
        Task<PermissionModel> AddPermissionAsync([NotNull] PermissionModel permission, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para atualização de uma nova permissão.
        /// </summary>
        /// <param name="permission">Dados da permissão à ser atualizada.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados da permissão atualizada.</returns>
        Task<PermissionModel> UpdatePermissionAsync([NotNull] PermissionModel permission, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para remoção de uma nova permissão.
        /// </summary>
        /// <param name="id">Id da permissão à ser removida.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados da permissão removida.</returns>
        Task<PermissionModel> RemovePermissionAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para remoção de uma nova permissão.
        /// </summary>
        /// <param name="key">Chave da permissão à ser removida.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados da permissão removida.</returns>
        Task<PermissionModel> RemovePermissionAsync([NotNull] string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para remoção de uma nova permissão.
        /// </summary>
        /// <param name="permission">Dados da permissão à ser removida.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados da permissão removida.</returns>
        Task<PermissionModel> RemovePermissionAsync([NotNull] PermissionModel permission, CancellationToken cancellationToken = default);
    }
}
