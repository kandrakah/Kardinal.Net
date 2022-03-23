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
    /// Dados de tipo zip.
    /// </summary>
    public class Zip : FileType
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        public Zip() : this(4, "application/zip", "zip")
        {
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="headerLength">Tamanho do cabeçalho do tipo de arquivo.</param>
        /// <param name="mediaType">Definição do tipo de mídia.</param>
        /// <param name="extension">Extensão conhecida do tipo de arquivo.</param>
        protected Zip(int headerLength, string mediaType, string extension) : base(new byte[] { 0x50, 0x4B, 0x03, 0x04 }, headerLength, mediaType, extension)
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
