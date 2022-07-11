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

namespace Kardinal.Net.Web.Auth
{
    /// <summary>
    /// Entidade de grupo de permissões.
    /// </summary>
    public class PermissionGroupEntity : Entity
    {
        /// <summary>
        /// Nome apresentável do grupo de permissões.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Descrição do grupo de permissões.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indica que se trata de um grupo de permissões restrito.
        /// </summary>
        public bool IsRestricted { get; set; }

        /// <summary>
        /// Relacionamento com permissões. Veja <see cref="PermissionEntity"/>
        /// </summary>
        public IEnumerable<PermissionEntity> Permissions { get; set; }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}
