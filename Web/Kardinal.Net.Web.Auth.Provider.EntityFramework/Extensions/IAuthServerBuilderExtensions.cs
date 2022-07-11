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

namespace Kardinal.Net.Web.Auth
{
    /// <summary>
    /// Extensões para <see cref="IAuthServerBuilder"/>
    /// </summary>
    public static class IAuthServerBuilderExtensions
    {
        /// <summary>
        /// Extensão para adicionar os stores do serviço de identidade.
        /// </summary>
        /// <param name="builder">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IAuthServerBuilder AddDefaultStores(this IAuthServerBuilder builder)
        {
            builder.AddPermissionsStore<EntityFrameworkPermissionsStore>();
            return builder;
        }
    }
}
