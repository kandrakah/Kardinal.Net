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
    /// Classe de extensões para <see cref="IConfiguration"/>.
    /// </summary>
    public static class IConfigurationExtensions
    {
        /// <summary>
        /// Extensão que traz as configurações tipadas da configuração.
        /// </summary>
        /// <typeparam name="T">Objeto contendo as configurações da sessão</typeparam>
        /// <param name="configuration">Instância de <see cref="IConfiguration"/></param>
        /// <returns>Objeto contendo as configurações.</returns>
        public static T GetOptions<T>(this IConfiguration configuration) where T : class
        {
            var sectionName = GetSectionName<T>();
            return configuration.GetOptions<T>(sectionName);
        }


        /// <summary>
        /// Extensão que traz as configurações tipadas da configuração.
        /// </summary>
        /// <typeparam name="T">Objeto contendo as configurações da sessão</typeparam>
        /// <param name="configuration">Instância de <see cref="IConfiguration"/></param>
        /// <param name="sectionName">Nome da sessão de configurações</param>
        /// <returns>Objeto contendo as configurações.</returns>
        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class
        {
            var section = configuration.GetSection(sectionName);
            var settings = section?.GetOptions<T>();
            return settings ?? Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Método que obtém o nome da sessão de opções.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de configurações.</typeparam>
        /// <returns>Nome da sessão de opções.</returns>
        private static string GetSectionName<T>() where T : class
        {
            if (typeof(T) == typeof(AbstractOptions))
            {
                var instance = Activator.CreateInstance<T>() as AbstractOptions;
                return instance.SectionName;
            }
            else
            {
                return typeof(T).Name.Replace("options", string.Empty, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
