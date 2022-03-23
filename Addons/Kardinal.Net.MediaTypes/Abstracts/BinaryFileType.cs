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

using OpenMcdf;
using System;
using System.IO;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe abstrata de tipo de arquivos binários.
    /// </summary>
    public abstract class BinaryFileType : FileType, IFileFormatReader
    {
        /// <summary>
        /// Entrada no armazenamento estruturado que é usada para identificar o formato.
        /// </summary>
        public string Storage { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="storage">Entrada no armazenamento estruturado que é usada para identificar o formato.</param>
        /// <param name="mediaType">Tipo de mídia do formato.</param>
        /// <param name="extension">Extensão conhecida do formato.</param>
        public BinaryFileType(string storage, string mediaType, string extension) : base(new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }, int.MaxValue, mediaType, extension)
        {
            if (string.IsNullOrEmpty(storage))
            {
                throw new ArgumentNullException(nameof(storage));
            }

            Storage = storage;
        }

        /// <summary>
        /// Método que verifica se o Stream de dados do arquivo é compatível com este tipo de arquivo.
        /// </summary>
        /// <param name="file">Dados do arquivo.</param>
        /// <returns>Verdadeiro caso o stream de dados do arquivo seja compatível com este tipo de arquivo e falso caso contrário.</returns>
        public bool IsMatch(IDisposable file)
        {
            if (file is CompoundFile cf)
            {
                return cf.RootStorage.TryGetStream(Storage, out CFStream stream);
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
        /// <returns>Instância dos dados do arquivo composto. Veja <see cref="CompoundFile"/></returns>
        public IDisposable Read(Stream stream)
        {
            try
            {
                return new CompoundFile(stream, CFSUpdateMode.ReadOnly, CFSConfiguration.LeaveOpen);
            }
            catch (EndOfStreamException)
            {
                return null;
            }
        }
    }
}
