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

namespace Kardinal.Net
{
    /// <summary>
    /// Enumerador que classifica a comparação entre versões.
    /// </summary>
    public enum VersionComparation
    {
        /// <summary>
        /// Comparação desconhecida.
        /// </summary>
        Unknow,

        /// <summary>
        /// Indica que a versão comparada é mais nova.
        /// </summary>
        Newer,

        /// <summary>
        /// Indica que a versão comparada é mais antiga.
        /// </summary>
        Older,

        /// <summary>
        /// Indica que a versão comparada é equivalente.
        /// </summary>
        Current
    }
}
