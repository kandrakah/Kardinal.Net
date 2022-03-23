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

using Kardinal.Net.MediaTypes.Localization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe abstrata para os tipos de arquivos.
    /// </summary>
    public abstract class FileType : IEquatable<FileType>
    {
        /// <summary>
        /// Dados da assinatura do tipo arquivo.
        /// </summary>
        public ReadOnlyCollection<byte> Signature { get; }

        /// <summary>
        /// Tamanho do cabeçalho do arquivo.
        /// </summary>
        public int HeaderLength { get; }

        /// <summary>
        /// Extensão conhecida do arquivo.
        /// </summary>
        public string Extension { get; }

        /// <summary>
        /// Definição do tipo de mídia.
        /// </summary>
        public string MediaType { get; }

        /// <summary>
        /// Offset de localização do cabeçalho do tipo de arquivo.
        /// </summary>
        public int Offset { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="mediaType">Definição do tipo de mídia.</param>
        protected FileType(string mediaType) : this(mediaType, null)
        {

        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="mediaType">Definição do tipo de mídia.</param>
        /// <param name="extension">Extensão conhecida do tipo de arquivo.</param>
        protected FileType(string mediaType, string extension)
        {
            if (string.IsNullOrEmpty(mediaType))
            {
                throw new ArgumentNullException(Resource.ERROR_FORMAT_MIMETYPE_NULL);
            }

            this.Signature = new ReadOnlyCollection<byte>(new List<byte>());
            this.HeaderLength = 0;
            this.Offset = 0;
            this.Extension = extension;
            this.MediaType = mediaType;
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="signature">Dados do cabeçalho do tipo de arquivo.</param>
        /// <param name="mediaType">Definição do tipo de mídia.</param>
        /// <param name="extension">Extensão conhecida do tipo de arquivo.</param>
        protected FileType(byte[] signature, string mediaType, string extension) : this(signature, signature == null ? 0 : signature.Length, mediaType, extension, 0)
        {
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="signature">Dados do cabeçalho do tipo de arquivo.</param>
        /// <param name="headerLength">Tamanho conhecido do cabeçalho do tipo de arquivo.</param>
        /// <param name="mediaType">Definição do tipo de mídia.</param>
        /// <param name="extension">Extensão conhecida do tipo de arquivo.</param>
        protected FileType(byte[] signature, int headerLength, string mediaType, string extension) : this(signature, headerLength, mediaType, extension, 0)
        {
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="signature">Dados do cabeçalho do tipo de arquivo.</param>
        /// <param name="mediaType">Definição do tipo de mídia.</param>
        /// <param name="extension">Extensão conhecida do tipo de arquivo.</param>
        /// <param name="offset">Offset de localização do cabeçalho do tipo de arquivo.</param>
        protected FileType(byte[] signature, string mediaType, string extension, int offset) : this(signature, signature == null ? offset : signature.Length + offset, mediaType, extension, offset)
        {
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="signature">Dados do cabeçalho do tipo de arquivo.</param>
        /// <param name="headerLength">Tamanho conhecido do cabeçalho do tipo de arquivo.</param>
        /// <param name="mediaType">Definição do tipo de mídia.</param>
        /// <param name="extension">Extensão conhecida do tipo de arquivo.</param>
        /// <param name="offset">Offset de localização do cabeçalho do tipo de arquivo.</param>
        protected FileType(byte[] signature, int headerLength, string mediaType, string extension, int offset)
        {
            if (signature == null || signature.Length == 0)
            {
                throw new ArgumentNullException(Resource.ERROR_FORMAT_SIGNATURE_NULL);
            }

            if (string.IsNullOrEmpty(mediaType))
            {
                throw new ArgumentNullException(Resource.ERROR_FORMAT_MIMETYPE_NULL);
            }

            this.Signature = new ReadOnlyCollection<byte>(signature);
            this.HeaderLength = headerLength;
            this.Offset = offset;
            this.Extension = extension;
            this.MediaType = mediaType;
        }

        /// <summary>
        /// Método que verifica se o Stream de dados do arquivo é compatível com este tipo de arquivo.
        /// </summary>
        /// <param name="stream">Stream de dados do arquivo.</param>
        /// <returns>Verdadeiro caso o stream de dados do arquivo seja compatível com este tipo de arquivo e falso caso contrário.</returns>
        public virtual bool IsMatch(Stream stream)
        {
            if (stream == null || (stream.Length < HeaderLength && HeaderLength < int.MaxValue) || Offset > stream.Length)
            {
                return false;
            }

            stream.Position = Offset;

            for (int i = 0; i < this.Signature.Count; i++)
            {
                var b = stream.ReadByte();
                if (b != this.Signature[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Método para verfificar se um objeto é igual à este.
        /// </summary>
        /// <param name="obj">Objeto à ser comparado.</param>
        /// <returns>Verdadeiro caso os objetos sejam iguais e falso caso contrário.</returns>
        public override bool Equals(object obj)
        {
            return obj is FileType fileType ? Equals(fileType) : false;
        }

        /// <summary>
        /// Método para verfificar se um objeto é igual à este.
        /// </summary>
        ///<param name="other">Objeto à ser comparado.</param>
        /// <returns>Verdadeiro caso os objetos sejam iguais e falso caso contrário.</returns>
        public bool Equals(FileType other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return other.Signature.SequenceEqual(Signature);
        }

        /// <summary>
        /// Método que calcula o hashCode desta instância.
        /// </summary>
        /// <returns>HashCode da instância dessa classe.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                if (Signature == null)
                {
                    return 0;
                }

                int hash = 17;
                foreach (var signature in Signature)
                {
                    hash = hash * 31 + signature.GetHashCode();
                }

                return hash;
            }
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
