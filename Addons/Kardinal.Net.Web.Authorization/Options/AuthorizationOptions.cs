﻿/*
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
    /// Classe de opções de autorização.
    /// </summary>
    public class AuthorizationOptions : AbstractOptions
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        public AuthorizationOptions() : base("Authorization") { }

        /// <summary>
        /// Configurações de identidade.
        /// </summary>
        public KardinalIdentityOptions Identity { get; set; } = new KardinalIdentityOptions();
    }
}