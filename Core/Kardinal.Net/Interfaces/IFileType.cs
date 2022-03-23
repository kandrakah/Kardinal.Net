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

using System.Collections.ObjectModel;
using System.IO;

namespace Kardinal.Net
{
    /// <summary>
    /// Interface de tipo de arquivo.
    /// </summary>
    public interface IFileType
    {
        /// <summary>
        /// dados binários de assinatura do formato de arquivo.
        /// </summary>
        ReadOnlyCollection<byte> Signature { get; }

        /// <summary>
        /// Comprimento do header da assinatura.
        /// </summary>
        int HeaderLength { get; }

        /// <summary>
        /// Extensão de aquivo geralmente associada ao arquivo.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Declaração de tipo de mídia do arquivo.
        /// </summary>
        string MediaType { get; }

        /// <summary>
        /// Espaçamento da localização da assinatura do arquivo.
        /// </summary>
        int Offset { get; }

        /// <summary>
        /// Returns a value indicating whether the format matches a file header.
        /// </summary>
        bool IsMatch(Stream stream);
    }
}
