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

using Microsoft.Extensions.Configuration;
using System;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de extensões para <see cref="IConfigurationSection"/>.
    /// </summary>
    public static partial class IConfigurationSectionExtensions
    {
        /// <summary>
        /// Extensão que traz as configurações tipadas da sessão.
        /// </summary>
        /// <typeparam name="T">Objeto contendo as configurações da sessão.</typeparam>
        /// <param name="section">Este Objeto.</param>
        /// <returns>Instância das configurações.</returns>
        public static T GetOptions<T>(this IConfigurationSection section) where T : class
        {
            var options = section?.Get<T>();
            if (options == null)
            {
                options = Activator.CreateInstance<T>();
            }
            return options;
        }
    }
}
