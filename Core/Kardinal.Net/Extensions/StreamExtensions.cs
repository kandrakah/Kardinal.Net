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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;

namespace Kardinal.Net
{
    /// <summary>
    /// Extensões para <see cref="Stream"/>
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Extensão para converter o Stream em um vetor de bytes.
        /// </summary>
        /// <param name="stream">Objeto referenciado.</param>
        /// <returns>Vetor de bytes referente ao stream.</returns>
        public static byte[] ToByteArray([NotNull] this Stream stream)
        {
            if (stream == null)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Extensão que computa um hash de um stream de dados.
        /// </summary>
        /// <param name="source">Objeto referenciado</param>
        /// <param name="hashAlgoritm">Algoritmo utilizado no cálculo de hash. Veja <see cref="HashAlgorithmName"/></param>
        /// <returns>Hash gerado para o stream de dados informado</returns>
        public static byte[] ComputeHash(this Stream source, HashAlgorithmName hashAlgoritm)
        {
            var hasher = default(HashAlgorithm);
            switch (hashAlgoritm.Name)
            {
                case "MD5":
                    hasher = MD5.Create();
                    break;
                case "SHA1":
                    hasher = SHA1.Create();
                    break;
                case "SHA256":
                    hasher = SHA256.Create();
                    break;
                case "SHA384":
                    hasher = SHA384.Create();
                    break;
                case "SHA512":
                    hasher = SHA512.Create();
                    break;
                default:
                    break;
            }

            return hasher.ComputeHash(source);
        }

        /// <summary>
        /// Extensão para salvar um fluxo de dados em arquivo.
        /// </summary>
        /// <param name="stream">Este Stream</param>
        /// <param name="filePath">Diretório onde o stream será salvo junto com o nome do arquivo</param>
        /// <param name="fileMode">Modo de abertura do arquivo. Veja <see cref="FileMode"/></param>
        /// <param name="fileAccess">Modo de acesso ao arquivo. Veja <see cref="FileAccess"/></param>
        public static void WriteToFile(this Stream stream, string filePath, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.ReadWrite)
        {
            var data = stream.ToByteArray();

            if (data == null)
            {
                data = new byte[0];
            }

            using (var file = new FileStream(filePath, fileMode, fileAccess))
            {
                file.Write(data, 0, data.Length);
                file.Flush();
            }
        }
    }
}