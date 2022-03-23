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

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de configurações do usuário de sistema.
    /// </summary>
    public class CurrentUserOptions : AbstractOptions
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        public CurrentUserOptions() : base("CurrentUser")
        {

        }

        /// <summary>
        /// Definições das claims.
        /// </summary>
        public CurrentUserClaimsOptions Claims { get; set; } = new CurrentUserClaimsOptions();

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return $"[CurrentUser]";
        }
    }
}
