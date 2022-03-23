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

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Kardinal.Net
{
    /// <summary>
    /// Extensões para <see cref="byte"/>.
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Extensão que computa um hash de um vetor de bytes.
        /// </summary>
        /// <param name="bytes">Objeto referenciado</param>
        /// <param name="hashAlgoritmName">Algoritmo utilizado no cálculo de hash. Veja <see cref="HashAlgorithmName"/></param>
        /// <returns>Hash gerado para o byte array informado</returns>
        public static byte[] ComputeHash([NotNull] this byte[] bytes, HashAlgorithmName hashAlgoritmName)
        {
            var hasher = default(HashAlgorithm);
            switch (hashAlgoritmName.Name)
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
                    throw new ArgumentException("O algoritmo informado não é suportado.");
            }

            return hasher.ComputeHash(bytes);
        }

        /// <summary>
        /// Extensão para conversão de um vetor de bytes em um fluxo de dados.
        /// </summary>
        /// <param name="bytes">Objeto referenciado.</param>
        /// <returns>Instância de um fluxo de dados baseado nos bytes informados.</returns>
        public static Stream ToStream([NotNull] this byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        /// <summary>
        /// Extensão para conversão de um vetor de bytes em Base64;
        /// </summary>
        /// <param name="bytes">Objeto referenciado.</param>
        /// <returns>Valor em Base64 do vetor de bytes informado.</returns>
        public static string ToBase64([NotNull] this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Extensão para conversão de um vetor de bytes em uma string hexadecimal.
        /// </summary>
        /// <param name="bytes">Objeto referenciado.</param>
        /// <returns>Valor em hexadecimal do vetor de bytes informado.</returns>
        public static string ToHex([NotNull] this byte[] bytes)
        {
            return Convert.ToHexString(bytes);
        }
    }
}
