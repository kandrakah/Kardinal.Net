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
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe abstrata de definição de tipos de arquivo OpenXmlDocument.
    /// </summary>
    public abstract class OpenXmlDocument : Zip, IFileFormatReader
    {
        /// <summary>
        /// Identificação de tipo de dados do tipo de arquivo.
        /// </summary>
        public string IdentifiableEntry { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="identifiableEntry">Identificação de tipo de dados do tipo de arquivo.</param>
        /// <param name="mediaType">Definição do tipo de mídia.</param>
        /// <param name="extension">Extensão conhecida do tipo de arquivo.</param>
        protected OpenXmlDocument(string identifiableEntry, string mediaType, string extension) : base(int.MaxValue, mediaType, extension)
        {
            if (string.IsNullOrEmpty(identifiableEntry))
            {
                throw new ArgumentNullException(nameof(identifiableEntry));
            }

            IdentifiableEntry = identifiableEntry;
        }

        /// <summary>
        /// Método que verifica se o Stream de dados do arquivo é compatível com este tipo de arquivo.
        /// </summary>
        /// <param name="file">dados do arquivo.</param>
        /// <returns>Verdadeiro caso o stream de dados do arquivo seja compatível com este tipo de arquivo e falso caso contrário.</returns>
        public bool IsMatch(IDisposable file)
        {
            if (file is ZipArchive archive)
            {
                return archive.Entries.Any(e => e.FullName.Equals(IdentifiableEntry, StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método que efetua a leitura do arquivo binário.
        /// </summary>
        /// <param name="stream">Stream de dados do arquivo.</param>
        /// <returns>Instância dos dados do arquivo composto. Veja <see cref="IDisposable"/></returns>
        public IDisposable Read(Stream stream)
        {
            try
            {
                return new ZipArchive(stream, ZipArchiveMode.Read, true);
            }
            catch (InvalidDataException)
            {
                return null;
            }
        }
    }
}
