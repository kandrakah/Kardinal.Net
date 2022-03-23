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
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Kardinal.Net.Blockchain
{
    /// <summary>
    /// Implementação do serializador de blockchain para xml.
    /// </summary>
    internal class DefaultXmlSimpleBlockchainDataSerializer : ISimpleBlockchainDataSerializer
    {
        /// <summary>
        /// Método que desserializa uma entrada em xml em um blockchain.
        /// </summary>
        /// <param name="data">Dados à serem desserializados.</param>
        /// <returns>Blockchain resultante da desserialização. Veja <see cref="SimpleBlockchain"/></returns>
        public static SimpleBlockchain Parse([NotNull] string data)
        {
            var serializer = new DefaultXmlSimpleBlockchainDataSerializer();
            var value = serializer.Deserialize(data);
            value.Validate();
            return value;
        }

        /// <summary>
        /// Método que faz a tentativa de desserialização do blockchain.
        /// </summary>
        /// <param name="data">Dados à serem desserializados.</param>
        /// <param name="blockchain">Blockchain resultante da desserialização. Veja <see cref="SimpleBlockchain"/></param>
        /// <returns>Verdadeiro caso a desserialização seja bem sucedida e falso caso contrário.</returns>
        public static bool TryParse(string data, out SimpleBlockchain blockchain)
        {
            try
            {
                blockchain = Parse(data);
                return true;
            }
            catch
            {
                blockchain = SimpleBlockchain.New();
                return false;
            }
        }

        /// <summary>
        /// Método que verifica se o objeto pode ser serializado.
        /// </summary>
        /// <typeparam name="T">ipo do objeto à ser serializado.</typeparam>
        /// <param name="obj">Objeto à ser serializado.</param>
        /// <returns>Verdadeiro caso o objeto possa ser serializado e falso caso contrário.</returns>
        public bool CanSerialize<T>(T obj)
        {
            return obj != null;
        }

        /// <summary>
        /// Método que verifica se o objeto pode ser desserializado.
        /// </summary>
        /// <typeparam name="T">ipo do objeto à ser serializado.</typeparam>
        /// <param name="data">Dados à serem desserializados.</param>
        /// <returns>Verdadeiro caso o objeto possa ser desserializado e falso caso contrário.</returns>
        public bool CanDeserialize<T>(string data)
        {
            using (var reader = new StringReader(data))
            {
                var xml = XmlReader.Create(reader);
                var serializer = new XmlSerializer(typeof(T));
                return serializer.CanDeserialize(xml);
            }
        }

        /// <summary>
        /// Método para serialização de dados.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto à ser serializado.</typeparam>
        /// <param name="obj">Objeto à ser serializado.</param>
        /// <returns>Valor serializado do objeto.</returns>
        public string SerializeChainLink<T>(T obj)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, obj);
                var itemData = Encoding.UTF8.GetString(stream.ToArray());
                return itemData;
            }
        }

        /// <summary>
        /// Método para desserialziação de dados.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto à ser desserialziado.</typeparam>
        /// <param name="data">Dados do objeto à ser desserializado.</param>
        /// <returns>Objeto desserializado.</returns>
        public T DesserializeChainLink<T>(string data)
        {
            using (var reader = new StringReader(data))
            {
                var desserializer = new XmlSerializer(typeof(T));
                var item = (T)desserializer.Deserialize(reader);
                return item;
            }
        }

        /// <summary>
        /// Método que efetua a serialização do blockchain.
        /// </summary>
        /// <param name="blockchain">Blockchain à ser serializado.</param>
        /// <param name="chainLinks">Enumeração de elos do blockchain.</param>
        /// <returns>Forma serializada do blockchain.</returns>
        public string Serialize([NotNull] SimpleBlockchain blockchain, [NotNull] IEnumerable<ChainLink> chainLinks)
        {
            if (chainLinks == null || !chainLinks.Any())
            {
                throw new BlockchainSerializationException(Resource.ERROR_BLOCKCHAIN_NO_LINKS);
            }

            var data = new SimpleBlockchainSerializedModel()
            {
                BlockchainId = blockchain.BlockchainId,
                FirstIndex = chainLinks.OrderBy(x => x.Index).Select(x => x.Index).First(),
                LastIndex = chainLinks.OrderBy(x => x.Index).Select(x => x.Index).Last(),
                ChainLinks = new List<SimpleChainLinkSerializedModel>()
            };

            foreach (var link in chainLinks)
            {
                if (blockchain.BlockchainId != link.BlockchainId)
                {
                    throw new BrokenChainException(Resource.ERROR_BLOCKCHAIN_INVALID_LINK.SetParameters("index", link.Index).SetParameters("hash", link.Hash));
                }

                var item = new SimpleChainLinkSerializedModel()
                {
                    Index = link.Index,
                    Timestamp = link.Timestamp.Ticks,
                    Data = link.Data,
                    PreviousHash = link.PreviousHash,
                    Hash = link.Hash
                };

                data.ChainLinks.Add(item);
            }

            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(SimpleBlockchainSerializedModel));
                serializer.Serialize(stream, data);
                var itemData = Encoding.UTF8.GetString(stream.ToArray());
                return itemData;
            }
        }

        /// <summary>
        /// Método que efetua a serialização do blockchain.
        /// </summary>        
        /// <returns>Forma serializada do blockchain.</returns>
        public SimpleBlockchain Deserialize([NotNull] string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new BlockchainSerializationException(Resource.ERROR_BLOCKCHAIN_INVALID_DESSERIALIZATION);
            }

            if (!this.CanDeserialize<SimpleBlockchainSerializedModel>(data))
            {
                throw new BlockchainSerializationException(Resource.ERROR_BLOCKCHAIN_INVALID_DESSERIALIZATION);
            }

            using (var reader = new StringReader(data))
            {
                var desserializer = new XmlSerializer(typeof(SimpleBlockchainSerializedModel));
                var model = (SimpleBlockchainSerializedModel)desserializer.Deserialize(reader);
                var links = model.ChainLinks.OrderBy(x => x.Index).Select(x => ChainLink.NewChainLink(model.BlockchainId, x.Index, new DateTime(x.Timestamp), x.Data, x.Hash, x.PreviousHash)).ToList();

                var blockchain = SimpleBlockchain.NewInitializedBlockChain(model.BlockchainId, links);
                return blockchain;
            }
        }
    }
}
