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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Kardinal.Net
{
    /// <summary>
    /// Implementação do serviço de detecção de tipos de arquivo.
    /// </summary>
    public class FileDetector : IFileDetector
    {
        private readonly IEnumerable<FileType> _formats;

        /// <summary>
        /// Método construtor.
        /// </summary>
        public FileDetector() : this(TypeLoader.GetTypeInfos(typeof(FileDetector).GetTypeInfo().Assembly))
        {
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="assembly">Assembly onde se localiza as implementações dos tipos de arquivo.</param>
        public FileDetector([NotNull] Assembly assembly) : this(TypeLoader.GetTypeInfos(assembly))
        {
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="formats">Enumeração de tipos de arquivos.</param>
        public FileDetector([NotNull] IEnumerable<FileType> formats)
        {
            this._formats = formats ?? throw new ArgumentNullException(nameof(formats));
        }

        /// <summary>
        /// Método para detecção do tipo do arquivo.
        /// </summary>
        /// <param name="data">Vetor de bytes do arquivo.</param>
        /// <returns>Tipo do arquivo detectado.</returns>
        public FileType Detect(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                return this.Detect(stream);
            }
        }

        /// <summary>
        /// Método para detecção do tipo do arquivo.
        /// </summary>
        /// <param name="stream">Stream de dados do arquivo.</param>
        /// <returns>Tipo do arquivo detectado.</returns>
        public FileType Detect(Stream stream)
        {
            return this.Detect(stream, this._formats.ToArray());
        }

        /// <summary>
        /// Método para detecção do tipo do arquivo.
        /// </summary>
        /// <param name="data">Vetor de bytes do arquivo.</param>
        /// <param name="formats">Formatos de arquivo à serem usados na verificação.</param>
        /// <returns>Tipo do arquivo detectado.</returns>
        public FileType Detect(byte[] data, params FileType[] formats)
        {
            using (var stream = new MemoryStream(data))
            {
                return this.Detect(stream, formats);
            }
        }

        /// <summary>
        /// Método para detecção do tipo do arquivo.
        /// </summary>
        /// <param name="stream">Stream de dados do arquivo.</param>
        /// <param name="formats">Formatos de arquivo à serem usados na verificação.</param>
        /// <returns>Tipo do arquivo detectado.</returns>
        public FileType Detect(Stream stream, params FileType[] formats)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanSeek)
            {
                throw new NotSupportedException(Resource.ERROR_FORMAT_TYPES_NULL);
            }

            if (stream.Length == 0)
            {
                return null;
            }

            var matches = this.FindFormats(stream, formats);

            if (matches.Count() > 1)
            {
                this.ClearFormats(matches.ToList());
            }

            if (matches.Count() > 0)
            {
                return matches.OrderByDescending(m => m.HeaderLength).First();
            }

            return null;
        }

        /// <summary>
        /// Método que verifica se o arquivo é do tipo especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de dados à ser checado.</typeparam>
        /// <param name="data">Vetor de bytes do arquivo.</param>
        /// <returns>Verdadeiro caso o aquivo seja do tipo informado e falso caso contrário.</returns>
        public bool IsOfType<T>(byte[] data) where T : FileType
        {
            using (var stream = new MemoryStream(data))
            {
                return this.IsOfType<T>(stream);
            }
        }

        /// <summary>
        /// Método que verifica se o arquivo é do tipo especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de dados à ser checado.</typeparam>
        /// <param name="stream">Stream de dados do arquivo.</param>
        /// <returns>Verdadeiro caso o aquivo seja do tipo informado e falso caso contrário.</returns>
        public bool IsOfType<T>(Stream stream) where T : FileType
        {
            var isRegistered = false;

            var detectType = typeof(T);
            if (detectType.IsAbstract)
            {
                isRegistered = this._formats.Any(x => x.GetType().GetTypeInfo().IsSubclassOf(detectType));
            }
            else
            {
                isRegistered = this._formats.Any(x => x.GetType() == detectType);
            }

            if (!isRegistered)
            {
                throw new ArgumentException("Tipo de dados não registrado.");
            }

            var type = this.Detect(stream);
            return type is T;
        }

        /// <summary>
        /// Método que verifica se o arquivo é do tipo especificado.
        /// </summary>
        /// <param name="data">Vetor de bytes do arquivo.</param>
        /// <param name="mediaTypes">Tipos de mídia à serem verificados.</param>
        /// <returns>Verdadeiro caso o aquivo seja de um dos tipos informados e falso caso contrário.</returns>
        public bool IsOfType(byte[] data, params string[] mediaTypes)
        {
            var formats = this._formats.Where(x => mediaTypes.Contains(x.MediaType)).ToArray();
            if (!formats.Any())
            {
                throw new ArgumentException("Nenhum tipo de dados corresponde aos parâmetros informados.");
            }
            return this.IsOfType(data, formats);
        }

        /// <summary>
        /// Método que verifica se o arquivo é do tipo especificado.
        /// </summary>
        /// <param name="stream">Stream de dados do arquivo.</param>
        /// <param name="mediaTypes">Tipos de mídia à serem verificados.</param>
        /// <returns>Verdadeiro caso o aquivo seja de um dos tipos informados e falso caso contrário.</returns>
        public bool IsOfType(Stream stream, params string[] mediaTypes)
        {
            var formats = this._formats.Where(x => mediaTypes.Contains(x.MediaType)).ToArray();
            if (!formats.Any())
            {
                throw new ArgumentException("Nenhum tipo de dados corresponde aos parâmetros informados.");
            }
            return this.IsOfType(stream, formats);
        }

        /// <summary>
        /// Método que verifica se o arquivo é do tipo especificado.
        /// </summary>
        /// <param name="data">Vetor de bytes do arquivo.</param>
        /// <param name="formats">Formatos à serem verificados.</param>
        /// <returns>Verdadeiro caso o aquivo seja de um dos tipos informados e falso caso contrário.</returns>
        public bool IsOfType(byte[] data, params FileType[] formats)
        {
            using (var stream = new MemoryStream(data))
            {
                return this.IsOfType(stream, formats);
            }
        }

        /// <summary>
        /// Método que verifica se o arquivo é do tipo especificado.
        /// </summary>
        /// <param name="stream">Stream de dados do arquivo.</param>
        /// <param name="formats">Formatos à serem verificados.</param>
        /// <returns>Verdadeiro caso o aquivo seja de um dos tipos informados e falso caso contrário.</returns>
        public bool IsOfType(Stream stream, params FileType[] formats)
        {
            var result = this.Detect(stream, formats);
            return result != null;
        }

        /// <summary>
        /// Método que efetua a busca dos formatos compatíveis com os dados informados.
        /// </summary>
        /// <param name="stream">Stream de dados do arquivo.</param>
        /// <param name="formats">Formatos à serem verificados.</param>
        /// <returns></returns>
        private IEnumerable<FileType> FindFormats(Stream stream, IEnumerable<FileType> formats)
        {
            var candidates = formats
                .OrderBy(t => t.HeaderLength)
                .ToList();

            for (int i = 0; i < candidates.Count; i++)
            {
                if (!candidates[i].IsMatch(stream))
                {
                    candidates.RemoveAt(i);
                    i--;
                }
            }

            if (candidates.Count > 1)
            {
                var readers = candidates.OfType<IFileFormatReader>().ToList();

                if (readers.Any())
                {
                    var file = readers[0].Read(stream);
                    foreach (var reader in readers)
                    {
                        if (!reader.IsMatch(file))
                        {
                            candidates.Remove((FileType)reader);
                        }
                    }
                }
            }

            stream.Position = 0;
            return candidates;
        }

        private void ClearFormats(List<FileType> candidates)
        {
            for (var i = 0; i < candidates.Count; i++)
            {
                for (var j = 0; j < candidates.Count; j++)
                {
                    if (i != j && candidates[j].GetType().IsAssignableFrom(candidates[i].GetType()))
                    {
                        candidates.RemoveAt(j);
                        i--; j--;
                    }
                }
            }
        }

        /// <summary>
        /// Método de dispersão da instância da classe.
        /// </summary>
        public void Dispose()
        {
            this._formats.ToList().Clear();
        }
    }
}
