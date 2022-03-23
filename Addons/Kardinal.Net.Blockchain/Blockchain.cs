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

using Kardinal.Net.Blockchain.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Kardinal.Net.Blockchain
{
    /// <summary>
    /// Classe de funções estáticas de blockchain.
    /// </summary>
    public static class Blockchain
    {
        /// <summary>
        /// Dicionário de serializadores de blockchain.
        /// </summary>
        private static IDictionary<string, ISimpleBlockchainDataSerializer> Serializers = new Dictionary<string, ISimpleBlockchainDataSerializer>();

        internal static string DefaultSerializerKey { get; private set; }

        /// <summary>
        /// Método estático para registro de serializadores padrões de blockchain.
        /// </summary>
        static Blockchain()
        {
            RegisterSerializer<DefaultJsonSimpleBlockchainDataSerializer>(MediaTypeNames.Application.Json.ToUpper());
            RegisterSerializer<DefaultXmlSimpleBlockchainDataSerializer>(MediaTypeNames.Application.Xml.ToUpper());
            SetDefaultSerializer(MediaTypeNames.Application.Json.ToUpper());
        }

        /// <summary>
        /// Método que define o serializador padrão.
        /// </summary>
        /// <param name="key">Chave do serializador padrão.</param>
        public static void SetDefaultSerializer([NotNull] string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new BlockchainException(Resource.ERROR_SERIALIZER_KEY_NULL);
            }

            if (!Serializers.ContainsKey(key))
            {
                throw new BlockchainException(Resource.ERROR_SERIALIZER_KEY_NOT_FOUND);
            }

            DefaultSerializerKey = key.ToUpper();
        }

        /// <summary>
        /// Método que obtém o serializador padrão.
        /// </summary>
        /// <returns></returns>
        internal static ISimpleBlockchainDataSerializer GetDefaultSerializer()
        {
            return GetSerializer(DefaultSerializerKey);
        }

        internal static ISimpleBlockchainDataSerializer GetSerializer([NotNull] string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new BlockchainException(Resource.ERROR_SERIALIZER_KEY_NULL);
            }

            if (!Serializers.ContainsKey(key))
            {
                throw new BlockchainException(Resource.ERROR_SERIALIZER_KEY_NOT_FOUND);
            }

            return Serializers[key];
        }

        /// <summary>
        /// Método que efetua o registro de novos serializadores de blockchain.
        /// </summary>
        /// <typeparam name="T">Tipo do serializador à ser registrado.</typeparam>
        /// <param name="key">Chave de identificação do serializador à ser registrado.</param>
        /// <param name="setAsDefault">Indica que o serializador registrado deve ser marcado como o padrão.</param>
        public static void RegisterSerializer<T>([NotNull] string key, bool setAsDefault = false) where T : ISimpleBlockchainDataSerializer, new()
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new BlockchainException(Resource.ERROR_SERIALIZER_KEY_NULL);
            }

            var exists = Serializers.ContainsKey(key);
            if (exists)
            {
                Serializers[key] = new T();
            }
            else
            {
                Serializers.Add(key, new T());
            }

            if (setAsDefault)
            {
                SetDefaultSerializer(key);
            }
        }

        internal static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
            {
                return new byte[0];
            }

            var json = JsonSerializer.Serialize(obj);

            return json.ToHex().ToByteArray();
        }

        internal static T ByteArrayToObject<T>(byte[] data)
        {
            if (data == null)
            {
                return default(T);
            }

            var json = Encoding.Default.GetString(data).FromHex();

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
