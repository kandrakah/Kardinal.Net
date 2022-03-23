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

namespace Kardinal.Net
{
    /// <summary>
    /// Interface de definição do FileFormatReader.
    /// </summary>
    public interface IFileFormatReader
    {
        /// <summary>
        /// Método que verifica se o Stream de dados do arquivo é compatível com este tipo de arquivo.
        /// </summary>
        /// <param name="file">Stream de dados do arquivo.</param>
        /// <returns>Verdadeiro caso o stream de dados do arquivo seja compatível com este tipo de arquivo e falso caso contrário.</returns>
        bool IsMatch(IDisposable file);

        /// <summary>
        /// Método que efetua a leitura do arquivo binário.
        /// </summary>
        /// <param name="stream">Stream de dados do arquivo.</param>
        /// <returns>Instância dos dados do arquivo composto. Veja <see cref="Stream"/></returns>
        IDisposable Read(Stream stream);
    }
}
