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

using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Kardinal.Net.Web.Auth
{
    public class SystemDbContext : DbContext, IDataProtectionKeyContext
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="options">Opções do contexto.</param>
        public SystemDbContext([NotNull] DbContextOptions<SystemDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Coleção de entidades de chaves de segurança.
        /// </summary>
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Security
            modelBuilder.ApplyConfiguration(new DataProtectionKeyConfigurations());
            #endregion

            #region Permissions
            modelBuilder.ApplyConfiguration(new PermissionGroupConfigurations());
            modelBuilder.ApplyConfiguration(new PermissionConfigurations());

            #endregion
        }
    }
}
