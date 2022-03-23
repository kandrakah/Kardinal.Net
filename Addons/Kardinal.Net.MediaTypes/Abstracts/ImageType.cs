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

namespace Kardinal.Net
{
    /// <summary>
    /// Classe abstrata de tipos de formato de aquivo de imagen.
    /// </summary>
    public abstract class ImageType : FileType
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="signature">Dados do cabeçalho do tipo de arquivo.</param>
        /// <param name="mediaType">Definição do tipo de mídia.</param>
        /// <param name="extension">Extensão conhecida do tipo de arquivo.</param>
        protected ImageType(byte[] signature, string mediaType, string extension) : base(signature, mediaType, extension)
        {
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="signature">Dados do cabeçalho do tipo de arquivo.</param>
        /// <param name="mediaType">Definição do tipo de mídia.</param>
        /// <param name="extension">Extensão conhecida do tipo de arquivo.</param>
        /// <param name="offset">Offset de localização do cabeçalho do tipo de arquivo.</param>
        protected ImageType(byte[] signature, string mediaType, string extension, int offset) : base(signature, mediaType, extension, offset)
        {
        }
    }
}
