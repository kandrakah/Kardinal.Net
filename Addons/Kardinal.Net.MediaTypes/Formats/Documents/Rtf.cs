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

namespace Kardinal.Net
{
    /// <summary>
    /// Dados do tipo PDF.
    /// </summary>
    public class Rtf : FileType
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        public Rtf() : base(new byte[] { 0x7B, 0x5C, 0x72, 0x74, 0x66, 0x31 }, "application/rtf", "rtf")
        {
        }

        /// <summary>
        /// Método que retorna a representação string desta instância.
        /// </summary>
        /// <returns>Representação string da instância desta classe.</returns>
        public override string ToString()
        {
            return this.MediaType;
        }
    }
}
