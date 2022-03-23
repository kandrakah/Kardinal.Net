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

namespace Kardinal.Net.Blockchain
{
    /// <summary>
    /// Struct simples para blockchain.
    /// </summary>
    public struct SimpleBlockchain
    {
        /// <summary>
        /// Código de identificação único do blockchain.
        /// </summary>
        public string BlockchainId { get; }

        /// <summary>
        /// Lista de elos do blockchain.
        /// </summary>
        private readonly IList<ChainLink> _chainLinks;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="id">Id do blockchain.</param>
        internal SimpleBlockchain(string id)
        {
            this.BlockchainId = id;
            this._chainLinks = new List<ChainLink>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="blockchainId">Id do blockchain.</param>
        /// <param name="chainLinks">Elos do blockchain.</param>
        internal SimpleBlockchain(string blockchainId, IEnumerable<ChainLink> chainLinks)
        {
            this.BlockchainId = blockchainId;
            this._chainLinks = chainLinks.OrderBy(x => x.Index).ToList();
        }

        /// <summary>
        /// Método que faz a inicialização do blockchain.
        /// </summary>
        private void Initialize()
        {
            var genesis = ChainLink.NewChainLink(this.BlockchainId, 0, DateTime.Now, new byte[0]);
            this._chainLinks.Add(genesis);
        }

        internal static SimpleBlockchain NewInitializedBlockChain(string blockchainId, IEnumerable<ChainLink> chainLinks)
        {
            return new SimpleBlockchain(blockchainId, chainLinks);
        }

        /// <summary>
        /// Método estático que cria uma istância de um blockchain com serialização em formato json.
        /// </summary>
        /// <returns>Instância de um blockchain.</returns>
        public static SimpleBlockchain New()
        {
            var chain = new SimpleBlockchain(Guid.NewGuid().ToString().ToHex());
            chain.Initialize();
            return chain;
        }

        /// <summary>
        /// Método que adiciona um novo elo ao blockchain.
        /// </summary>
        /// <typeparam name="T">Tipo de dados à serem inseridos.</typeparam>
        /// <param name="item">Item à ser inserido.</param>
        /// <returns>Hash do item inserido.</returns>
        public string Add<T>([NotNull] T item)
        {
            var currentItem = this._chainLinks[this._chainLinks.Count - 1];
            var itemData = Blockchain.ObjectToByteArray(item);
            var chainLink = ChainLink.NewChainLink(this.BlockchainId, currentItem.Index + 1, DateTime.Now, itemData, currentItem.Hash);
            this._chainLinks.Add(chainLink);
            return chainLink.Hash;
        }

        /// <summary>
        /// Método que obtém um item pelo índice.
        /// </summary>
        /// <param name="index">Índice do item desejado.</param>
        /// <returns>Objeto referente ao índice indicado.</returns>
        public object Get(int index)
        {
            return this.Get<dynamic>(index);
        }

        /// <summary>
        /// Método que obtém um item pelo índice.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto à ser obtido.</typeparam>
        /// <param name="index">Índice do item desejado.</param>
        /// <returns>Objeto referente ao índice indicado.</returns>
        public T Get<T>(int index)
        {
            if (!this._chainLinks.Any(x => x.Index == index))
            {
                throw new IndexNotFoundException(Resource.ERROR_INDEX_NOT_FOUND);
            }
            var chainLink = this._chainLinks.Where(x => x.Index == index).FirstOrDefault();
            var item = Blockchain.ByteArrayToObject<T>(chainLink.Data);
            return item;
        }

        /// <summary>
        /// Método que obtém um item pelo hash.
        /// </summary>
        /// <param name="hash">Hash do item desejado.</param>
        /// <returns>Objeto referente ao índice indicado.</returns>
        public object Get(string hash)
        {
            return this.Get<object>(hash);
        }

        /// <summary>
        /// Método que obtém um item pelo hash.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto à ser obtido.</typeparam>
        /// <param name="hash">Hash do item desejado.</param>
        /// <returns>Objeto referente ao índice indicado.</returns>
        public T Get<T>(string hash)
        {
            if (!this._chainLinks.Any(x => x.Hash == hash))
            {
                throw new HashNotFoundException(Resource.ERROR_HASH_NOT_FOUND);
            }

            var chainLink = this._chainLinks.Where(x => x.Hash == hash).FirstOrDefault();
            var item = Blockchain.ByteArrayToObject<T>(chainLink.Data);
            return item;
        }

        /// <summary>
        /// Método que efetua a validação do blockchain.
        /// </summary>
        public void Validate()
        {
            for (int i = 1; i < this._chainLinks.Count; i++)
            {
                var currentBlock = this._chainLinks[i];
                var previousBlock = this._chainLinks[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    throw new BrokenChainException(Resource.ERROR_HASH_MISMATCH);
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    throw new BrokenChainException(Resource.ERROR_HASH_PREVIOUS_MISMATCH.SetParameters("index", i));
                }
            }
        }

        /// <summary>
        /// Método estático que cria uma instância de um blockchain utilizando o serializador padrão.
        /// </summary>
        /// <param name="data">Dados json do blockchain.</param>
        /// <returns><see cref="SimpleBlockchain"/> resultante dos dados.</returns>
        public static SimpleBlockchain Parse([NotNull] string data)
        {
            return Parse(data, Blockchain.DefaultSerializerKey);
        }

        /// <summary>
        /// Método estático que cria uma instância de um blockchain utilizando o serializador informado.
        /// </summary>
        /// <param name="data">Dados json do blockchain.</param>
        /// <param name="serializerKey">Chave de identificação do serializador à ser utilizado.</param>
        /// <returns><see cref="SimpleBlockchain"/> resultante dos dados.</returns>
        public static SimpleBlockchain Parse([NotNull] string data, string serializerKey)
        {
            var serializer = Blockchain.GetSerializer(serializerKey);
            return serializer.Deserialize(data);
        }

        /// <summary>
        /// Método estático que cria uma instância de um blockchain utilizando o serializador informado.
        /// </summary>
        /// <param name="data">Dados json do blockchain.</param>
        /// <param name="blockchain"><see cref="SimpleBlockchain"/> resultante dos dados.</param>        
        /// <returns>Verdadeiro caso os dados possam ser convertidos em um blockchain e falso caso contrário.</returns>
        public static bool TryParse([NotNull] string data, out SimpleBlockchain blockchain)
        {
            return TryParse(data, Blockchain.DefaultSerializerKey, out blockchain);
        }

        /// <summary>
        /// Método estático que cria uma instância de um blockchain utilizando o serializador informado.
        /// </summary>
        /// <param name="data">Dados json do blockchain.</param>
        /// <param name="serializerKey">Chave de identificação do serializador à ser utilizado.</param>
        /// <param name="blockchain"><see cref="SimpleBlockchain"/> resultante dos dados.</param>        
        /// <returns>Verdadeiro caso os dados possam ser convertidos em um blockchain e falso caso contrário.</returns>
        public static bool TryParse([NotNull] string data, string serializerKey, out SimpleBlockchain blockchain)
        {
            var serializer = Blockchain.GetSerializer(serializerKey);
            try
            {
                blockchain = serializer.Deserialize(data);
                return true;
            }
            catch
            {
                blockchain = New();
                return false;
            }
        }

        /// <summary>
        /// Método que faz a exportação do blockchain.
        /// </summary>
        /// <param name="stream">Stream de dados onde o blockchain será exportado.</param>
        public void Export(Stream stream)
        {
            var serializer = Blockchain.GetDefaultSerializer();
            var serialized = serializer.Serialize(this, this._chainLinks);
            var data = serialized.ToByteArray();
            stream.Write(data, 0, serialized.Length);
        }

        /// <summary>
        /// Método que faz a exportação do blockchain.
        /// </summary>
        /// <param name="stream">Stream de dados onde o blockchain será exportado.</param>
        /// <param name="serializerKey">Identificador do serializador de blockchain.</param>
        public void Export(Stream stream, string serializerKey)
        {
            var serializer = Blockchain.GetSerializer(serializerKey);
            var serialized = serializer.Serialize(this, this._chainLinks);
            var data = serialized.ToByteArray();
            stream.Write(data, 0, serialized.Length);
        }

        /// <summary>
        /// Método que retorna a representação string desta instância.
        /// </summary>
        /// <returns>Representação string da instância desta classe.</returns>
        public override string ToString()
        {
            return string.Join(".", this._chainLinks.OrderBy(x => x.Index).Select(x => x.Hash));
        }
    }
}
