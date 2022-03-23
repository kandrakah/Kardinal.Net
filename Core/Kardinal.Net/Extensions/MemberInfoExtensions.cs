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

using System;
using System.Reflection;

namespace Kardinal.Net.Extensions
{
    /// <summary>
    /// Extensões para <see cref="MemberInfo"/>.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Extensão que traz o nome qualificado do <see cref="MemberInfo"/> informado.
        /// </summary>
        /// <param name="memberInfo">Objeto referenciado</param>
        /// <returns>Nome qualificado do MemberInfo</returns>
        public static string GetQualifiedName(MemberInfo memberInfo)
        {
            if (memberInfo != null)
            {
                var type = memberInfo as Type;
                return type != null ? (type.Namespace + "." + type.Name) : memberInfo.Name;
            }
            else
            {
                return null;
            }
        }
    }
}
