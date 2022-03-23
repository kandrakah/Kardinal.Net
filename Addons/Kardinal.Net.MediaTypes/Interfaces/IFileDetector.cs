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
    /// Interface do serviço de detecção de tipos de arquivos.
    /// </summary>
    public interface IFileDetector : IDisposable
    {
        /// <summary>
        /// Método para detecção do tipo do arquivo.
        /// </summary>
        /// <param name="data">Vetor de bytes do arquivo.</param>
        /// <returns>Tipo do arquivo detectado.</returns>
        FileType Detect(byte[] data);

        /// <summary>
        /// Método para detecção do tipo do arquivo.
        /// </summary>
        /// <param name="stream">Stream de dados do arquivo.</param>
        /// <returns>Tipo do arquivo detectado.</returns>
        FileType Detect(Stream stream);

        /// <summary>
        /// Método para detecção do tipo do arquivo.
        /// </summary>
        /// <param name="data">Vetor de bytes do arquivo.</param>
        /// <param name="formats">Formatos de arquivo à serem usados na verificação.</param>
        /// <returns>Tipo do arquivo detectado.</returns>
        FileType Detect(byte[] data, params FileType[] formats);

        /// <summary>
        /// Método para detecção do tipo do arquivo.
        /// </summary>
        /// <param name="stream">Stream de dados do arquivo.</param>
        /// <param name="formats">Formatos de arquivo à serem usados na verificação.</param>
        /// <returns>Tipo do arquivo detectado.</returns>
        FileType Detect(Stream stream, params FileType[] formats);

        /// <summary>
        /// Método que verifica se o arquivo é do tipo especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de dados à ser checado.</typeparam>
        /// <param name="data">Vetor de bytes do arquivo.</param>
        /// <returns>Verdadeiro caso o aquivo seja do tipo informado e falso caso contrário.</returns>
        bool IsOfType<T>(byte[] data) where T : FileType;

        /// <summary>
        /// Método que verifica se o arquivo é do tipo especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de dados à ser checado.</typeparam>
        /// <param name="stream">Stream de dados do arquivo.</param>
        /// <returns>Verdadeiro caso o aquivo seja do tipo informado e falso caso contrário.</returns>
        bool IsOfType<T>(Stream stream) where T : FileType;

        /// <summary>
        /// Método que verifica se o arquivo é do tipo especificado.
        /// </summary>
        /// <param name="data">Vetor de bytes do arquivo.</param>
        /// <param name="mediaTypes">Tipos de mídia à serem verificados.</param>
        /// <returns>Verdadeiro caso o aquivo seja de um dos tipos informados e falso caso contrário.</returns>
        bool IsOfType(byte[] data, params string[] mediaTypes);

        /// <summary>
        /// Método que verifica se o arquivo é do tipo especificado.
        /// </summary>
        /// <param name="stream">Stream de dados do arquivo.</param>
        /// <param name="mediaTypes">Tipos de mídia à serem verificados.</param>
        /// <returns>Verdadeiro caso o aquivo seja de um dos tipos informados e falso caso contrário.</returns>
        bool IsOfType(Stream stream, params string[] mediaTypes);

        /// <summary>
        /// Método que verifica se o arquivo é do tipo especificado.
        /// </summary>
        /// <param name="data">Vetor de bytes do arquivo.</param>
        /// <param name="formats">Formatos à serem verificados.</param>
        /// <returns>Verdadeiro caso o aquivo seja de um dos tipos informados e falso caso contrário.</returns>
        bool IsOfType(byte[] data, params FileType[] formats);

        /// <summary>
        /// Método que verifica se o arquivo é do tipo especificado.
        /// </summary>
        /// <param name="stream">Stream de dados do arquivo.</param>
        /// <param name="formats">Formatos à serem verificados.</param>
        /// <returns>Verdadeiro caso o aquivo seja de um dos tipos informados e falso caso contrário.</returns>
        bool IsOfType(Stream stream, params FileType[] formats);
    }
}
