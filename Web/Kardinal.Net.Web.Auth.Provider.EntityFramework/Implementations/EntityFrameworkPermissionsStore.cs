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
using System.Diagnostics.CodeAnalysis;

namespace Kardinal.Net.Web.Auth
{
    public class EntityFrameworkPermissionsStore : AbstractService, IPermissionsStore
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="provider">Instância do provedor de serviços.</param>
        public EntityFrameworkPermissionsStore(IServiceProvider provider) : base(provider)
        {

        }

        /// <summary>
        /// Método para obtenção da listagem parinada de permissões.
        /// </summary>
        /// <param name="skip">Itens à serem ignorados.</param>
        /// <param name="take">Itens à serem obtidos.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Enumeração paginada de </returns>
        public Task<IPaginatedCollection<PermissionModel>> GetPermissionsAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            var request = new SearchRequestModel()
            {
                Skip = skip,
                Take = take
            };

            return this.GetPermissionsAsync(request, cancellationToken);
        }

        /// <summary>
        /// Método para obtenção da listagem parinada de permissões.
        /// </summary>
        /// <param name="ids">Ids das permissões à serem buscadas.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Enumeração paginada de </returns>
        public async Task<IPaginatedCollection<PermissionModel>> GetPermissionsAsync([NotNull] IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            if (ids == null)
            {
                throw new PermissionException("Identificadores de permissões nulos ou inválidos.");
            }

            using (var unitOfWork = this.GetService<IUnitOfWork<SystemDbContext>>())
            {
                var query = unitOfWork
                    .Set<PermissionEntity>()
                    .AsNoTracking()
                    .Where(x => ids.Contains(x.Id));

                return await this.GetPermissionsAsync(query, 0, ids.Count(), cancellationToken);
            }
        }

        /// <summary>
        /// Método para obtenção da listagem parinada de permissões.
        /// </summary>
        /// <param name="request">Dados de requisição de listagem de permissões.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Enumeração paginada de </returns>
        public async Task<IPaginatedCollection<PermissionModel>> GetPermissionsAsync([NotNull] SearchRequestModel request, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = this.GetService<IUnitOfWork<SystemDbContext>>())
            {
                var query = unitOfWork
                    .Set<PermissionEntity>()
                    .AsNoTracking()
                    .AsQueryable();

                if (request == null)
                {
                    throw new PermissionException("Requisição de permissões nulas ou inválidas.");
                }

                if (!string.IsNullOrEmpty(request.Terms))
                {
                    query = query.Where(x => x.DisplayName.Contains(request.Terms) || x.Key.Contains(request.Terms));
                }

                return await this.GetPermissionsAsync(query, request.Skip, request.Take, cancellationToken);
            }
        }

        /// <summary>
        /// Método para obtenção de uma permissão.
        /// </summary>
        /// <param name="id">Id da permissão à ser obtida.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados da permissão.</returns>
        public async Task<PermissionModel> GetPermissionAsync(Guid id, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = this.GetService<IUnitOfWork<SystemDbContext>>())
            {
                var query = unitOfWork
                    .Set<PermissionEntity>()
                    .AsNoTracking()
                    .Where(x => x.Id == id);

                var page = await this.GetPermissionsAsync(query, 0, 1, cancellationToken);
                var item = page.Items.FirstOrDefault();
                if (item == null)
                {
                    throw new PermissionException("A permissão solicitada não foi localizada.");
                }

                return item;
            }
        }

        /// <summary>
        /// Método para obtenção de uma permissão.
        /// </summary>
        /// <param name="key">Chave de identificação da permissão.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados da permissão.</returns>
        public async Task<PermissionModel> GetPermissionAsync([NotNull] string key, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = this.GetService<IUnitOfWork<SystemDbContext>>())
            {
                var query = unitOfWork
                    .Set<PermissionEntity>()
                    .AsNoTracking()
                    .Where(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));

                var page = await this.GetPermissionsAsync(query, 0, 1, cancellationToken);
                var item = page.Items.FirstOrDefault();
                if (item == null)
                {
                    throw new PermissionException("A permissão solicitada não foi localizada.");
                }

                return item;
            }
        }

        /// <summary>
        /// Método para obtenção dos dados da permissão.
        /// </summary>
        /// <param name="query">Query de permissões.</param>
        /// <param name="skip">Itens à serem ignorados.</param>
        /// <param name="take">Itens à serem obtidos.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Enumeração paginada de permissões.</returns>
        private async Task<IPaginatedCollection<PermissionModel>> GetPermissionsAsync([NotNull] IQueryable<PermissionEntity> query, int skip, int take, CancellationToken cancellationToken = default)
        {
            var permissions = await query
                .Select(x => new PermissionModel()
                {
                    Id = x.Id,
                    GroupId = x.GroupId,
                    Key = x.Key,
                    DisplayName = x.DisplayName,
                    Description = x.Description
                })
                .ToPaginatedListAsync(skip, take, cancellationToken);

            return permissions;
        }

        /// <summary>
        /// Método para adição de uma nova permissão.
        /// </summary>
        /// <param name="permission">Dados da permissão à ser adicionada.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados da permissão adicionada.</returns>
        public async Task<PermissionModel> AddPermissionAsync([NotNull] PermissionModel permission, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = this.GetService<IUnitOfWork<SystemDbContext>>())
            {
                await this.ValidateAsync(unitOfWork, permission, false, cancellationToken);

                var entity = new PermissionEntity()
                {
                    GroupId = permission.GroupId,
                    Key = permission.Key.ToUpper().Trim(),
                    DisplayName = permission.DisplayName,
                    Description = permission.Description
                };

                await unitOfWork
                    .Set<PermissionEntity>()
                    .AddAsync(entity, cancellationToken);

                permission.Id = entity.Id;
            }

            return permission;
        }

        /// <summary>
        /// Método para atualização de uma nova permissão.
        /// </summary>
        /// <param name="permission">Dados da permissão à ser atualizada.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados da permissão atualizada.</returns>
        public async Task<PermissionModel> UpdatePermissionAsync([NotNull] PermissionModel permission, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = this.GetService<IUnitOfWork<SystemDbContext>>())
            {
                await this.ValidateAsync(unitOfWork, permission, true, cancellationToken);

                var set = unitOfWork
                    .Set<PermissionEntity>();

                var entity = await set
                    .Where(x => x.Id == permission.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                entity.GroupId = permission.GroupId;
                entity.Key = permission.Key.ToUpper().Trim();
                entity.DisplayName = permission.DisplayName;
                entity.Description = permission.Description;

                await unitOfWork.SaveChangesAsync(cancellationToken);

                return permission;
            }
        }

        /// <summary>
        /// Método para remoção de uma nova permissão.
        /// </summary>
        /// <param name="id">Id da permissão à ser removida.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados da permissão removida.</returns>
        public async Task<PermissionModel> RemovePermissionAsync(Guid id, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = this.GetService<IUnitOfWork<SystemDbContext>>())
            {
                var set = unitOfWork
                    .Set<PermissionEntity>();

                var entity = await set
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    throw new PermissionException("A permissão não foi localizada.");
                }

                set.Remove(entity);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                var result = new PermissionModel()
                {
                    Id = entity.Id,
                    GroupId = entity.GroupId,
                    Key = entity.Key,
                    DisplayName = entity.DisplayName,
                    Description = entity.Description,
                };

                return result;
            }
        }

        /// <summary>
        /// Método para remoção de uma nova permissão.
        /// </summary>
        /// <param name="key">Chave da permissão à ser removida.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados da permissão removida.</returns>
        public async Task<PermissionModel> RemovePermissionAsync([NotNull] string key, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = this.GetService<IUnitOfWork<SystemDbContext>>())
            {
                var set = unitOfWork
                    .Set<PermissionEntity>();

                var entity = await set
                    .Where(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    throw new PermissionException("A permissão não foi localizada.");
                }

                set.Remove(entity);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                var result = new PermissionModel()
                {
                    Id = entity.Id,
                    GroupId = entity.GroupId,
                    Key = entity.Key,
                    DisplayName = entity.DisplayName,
                    Description = entity.Description,
                };

                return result;
            }
        }

        /// <summary>
        /// Método para remoção de uma nova permissão.
        /// </summary>
        /// <param name="permission">Dados da permissão à ser removida.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados da permissão removida.</returns>
        public async Task<PermissionModel> RemovePermissionAsync([NotNull] PermissionModel permission, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = this.GetService<IUnitOfWork<SystemDbContext>>())
            {
                var set = unitOfWork
                    .Set<PermissionEntity>();

                var entity = await set
                    .Where(x => x.Id == permission.Id && x.Key.Equals(permission.Key, StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    throw new PermissionException("A permissão não foi localizada.");
                }

                set.Remove(entity);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                var result = new PermissionModel()
                {
                    Id = entity.Id,
                    GroupId = entity.GroupId,
                    Key = entity.Key,
                    DisplayName = entity.DisplayName,
                    Description = entity.Description,
                };

                return result;
            }
        }

        /// <summary>
        /// Método que valida a requisição.
        /// </summary>
        /// <param name="unitOfWork">Instância da unidade de serviço.</param>
        /// <param name="permission">Dados da permissão à ser validada.</param>
        /// <param name="isUpdate">Indica que se trata de uma atualização.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        private async Task ValidateAsync(IUnitOfWork<SystemDbContext> unitOfWork, [NotNull] PermissionModel permission, bool isUpdate, CancellationToken cancellationToken)
        {
            var builder = this.CreateValidationBuilder("Falha ao validar o registro de permissão.");
            if (permission == null)
            {
                builder.Add("permission", "Requisição nula ou inválida.");
            }
            else
            {
                var set = unitOfWork
                    .Set<PermissionEntity>();

                if (isUpdate)
                {
                    var exists = await set
                        .AnyAsync(x => x.Id == permission.Id, cancellationToken);

                    if (!exists)
                    {
                        builder.Add("id", "A permissão não foi localizada.");
                    }
                }
                else
                {
                    var exists = await set
                        .AnyAsync(x => x.Id != permission.Id && x.Key.Equals(permission.Key, StringComparison.InvariantCultureIgnoreCase), cancellationToken);

                    if (!exists)
                    {
                        builder.Add("key", $"A chave de identificação {permission.Key} já se encontra cadastrada.");
                    }
                }

                if (string.IsNullOrEmpty(permission.Key))
                {
                    builder.Add("key", "A chave de identificação da permissão é requerida.");
                }

                if (string.IsNullOrEmpty(permission.DisplayName))
                {
                    builder.Add("displayName", "O nome da permissão é requerido.");
                }

                if (string.IsNullOrEmpty(permission.Description))
                {
                    builder.Add("description", "A descrição da permissão é requerida.");
                }

                var groupExists = await unitOfWork.Set<PermissionGroupEntity>()
                    .AnyAsync(x => x.Id == permission.GroupId, cancellationToken);

                if (!groupExists)
                {
                    builder.Add("groupId", "O grupo de permissões informado não foi localizado.");
                }
            }

            builder.Throw();
        }
    }
}
