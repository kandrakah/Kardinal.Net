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

namespace Kardinal.Net.Web.Authorization
{
    /// <summary>
    /// Enumerador para indicar o tipo de validação de múltiplas permissões.
    /// </summary>
    public enum PermissionValidationType
    {
        /// <summary>
        /// Indica possibilidade de acesso anônimo.
        /// </summary>
        Annonymous,
        /// <summary>
        /// Indica que o usuário ativo precisa apenas estar autenticado.
        /// </summary>
        RequireAuthenticatedOnly,
        /// <summary>
        /// Indica que o usuário ativo deve ter ao menos uma das permissões.
        /// </summary>
        RequireOneOf,

        /// <summary>
        /// Indica que o usuário ativo deve ter todas as permissões.
        /// </summary>
        RequireAll
    }
}
