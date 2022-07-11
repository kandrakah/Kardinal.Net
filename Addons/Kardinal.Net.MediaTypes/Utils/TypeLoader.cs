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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe estática para obtenção de perfis de tipos de arquivos.
    /// </summary>
    public static class TypeLoader
    {
        /// <summary>
        /// Método estático que obtém os tipos de arquivo de um assembly.
        /// </summary>
        /// <param name="assembly">Assembly onde as definições estão localizadas.</param>
        public static IEnumerable<FileType> GetTypeInfos([NotNull] Assembly assembly)
        {
            var result = assembly.GetTypes()
                .Where(t => typeof(FileType)
                .IsAssignableFrom(t))
                .Where(t => !t.GetTypeInfo().IsAbstract)
                .Where(t => t.GetConstructors().Any(c => c.GetParameters().Length == 0))
                .Select(t => Activator.CreateInstance(t))
                .OfType<FileType>()
                .OrderBy(x => x.MediaType)
                .ToList();

            return result;
        }
    }
}
