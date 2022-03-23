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
using System.Security.Cryptography;

namespace Kardinal.Net.Blockchain
{
    /// <summary>
    /// Struct de elo de uma blockchain.
    /// </summary>
    public struct ChainLink : IComparable<ChainLink>, IEquatable<ChainLink>, IEquatable<string>
    {
        /// <summary>
        /// Código de identificação do blockchain de origem do elo.
        /// </summary>
        public string BlockchainId { get; }

        /// <summary>
        /// Índice do item na blockchain.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Data de criação do registro.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Hash do registro anterior.
        /// </summary>
        public string PreviousHash { get; }

        /// <summary>
        /// Hash do item atual.
        /// </summary>
        public string Hash { get; }

        /// <summary>
        /// Dados atuais do elo.
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// Atributo que indica que se trata do elo inicial.
        /// </summary>
        internal bool IsGenesis => this.Index == 0;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="blockchainId">Código de identificação do blockchain de origem do elo.</param>
        /// <param name="index">Índice do elo no blockchain.</param>
        /// <param name="timestamp">Data de criação do registro.</param>
        /// <param name="data">Dados atuais do elo.</param>
        /// <param name="previousHash">Hash do registro anterior.</param>
        private ChainLink(string blockchainId, int index, DateTime timestamp, byte[] data, string previousHash = null)
        {
            this.BlockchainId = blockchainId;
            this.Index = index;
            this.Timestamp = timestamp;
            this.Data = data;
            this.PreviousHash = previousHash;
            this.Hash = $"{this.BlockchainId}-{this.Timestamp}-{this.PreviousHash ?? string.Empty}-{this.Data}".ComputeHash(HashAlgorithmName.SHA512).ToHex();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="blockchainId">Código de identificação do blockchain de origem do elo.</param>
        /// <param name="index">Índice do elo no blockchain.</param>
        /// <param name="timestamp">Data de criação do registro.</param>
        /// <param name="data">Dados atuais do elo.</param>
        /// <param name="dataHash">Hash do registro atual.</param>
        /// <param name="previousHash">Hash do registro anterior.</param>
        private ChainLink(string blockchainId, int index, DateTime timestamp, byte[] data, string dataHash, string previousHash)
        {
            this.BlockchainId = blockchainId;
            this.Index = index;
            this.Timestamp = timestamp;
            this.Data = data;
            this.PreviousHash = previousHash;
            this.Hash = dataHash;
        }

        /// <summary>
        /// Método estático que cria um novo elo de blockchain.
        /// </summary>
        /// <param name="blockchainId">Código de identificação do blockchain de origem do elo.</param>
        /// <param name="index">Índice do elo no blockchain.</param>
        /// <param name="timestamp">Data de criação do registro.</param>
        /// <param name="data">Dados atuais do elo.</param>
        /// <param name="previousHash">Hash do registro anterior.</param>
        /// <returns>Novo elo de blockchain.</returns>
        internal static ChainLink NewChainLink(string blockchainId, int index, DateTime timestamp, byte[] data, string previousHash = null)
        {
            return new ChainLink(blockchainId, index, timestamp, data, previousHash);
        }

        /// <summary>
        /// Método estático que cria um novo elo de blockchain.
        /// </summary>
        /// <param name="blockchainId">Código de identificação do blockchain de origem do elo.</param>
        /// <param name="index">Índice do elo no blockchain.</param>
        /// <param name="timestamp">Data de criação do registro.</param>
        /// <param name="data">Dados atuais do elo.</param>
        /// <param name="dataHash">Hash do registro atual.</param>
        /// <param name="previousHash">Hash do registro anterior.</param>
        /// <returns>Novo elo de blockchain.</returns>
        internal static ChainLink NewChainLink(string blockchainId, int index, DateTime timestamp, byte[] data, string dataHash, string previousHash)
        {
            return new ChainLink(blockchainId, index, timestamp, data, dataHash, previousHash);
        }

        /// <summary>
        /// Comparação de igualdade entre dois elos de blockchain.
        /// </summary>
        /// <param name="first">Primeiro elo à ser comparado.</param>
        /// <param name="second">Segundo elo à ser comparado.</param>
        /// <returns>Verdadeiro caso os elos sejam iguais e falso caso contrário.</returns>
        public static bool operator ==(ChainLink first, ChainLink second) => first.Equals(second);

        /// <summary>
        /// Comparação de diferença entre dois elos de blockchain.
        /// </summary>
        /// <param name="first">Primeiro elo à ser comparado.</param>
        /// <param name="second">Segundo elo à ser comparado.</param>
        /// <returns>Verdadeiro caso os elos sejam diferentes e falso caso contrário.</returns>
        public static bool operator !=(ChainLink first, ChainLink second) => !first.Equals(second);

        /// <summary>
        /// Comparação de maioridade entre dois elos de blockchain.
        /// </summary>
        /// <param name="first">Primeiro elo à ser comparado.</param>
        /// <param name="second">Segundo elo à ser comparado.</param>
        /// <returns>Verdadeiro caso o primeiro elo seja maior que o segundo e Falso caso contrário.</returns>
        public static bool operator >(ChainLink first, ChainLink second) => first.CompareTo(second) == 1;

        /// <summary>
        /// Comparação de minoridade entre dois elos de blockchain.
        /// </summary>
        /// <param name="first">Primeiro elo à ser comparado.</param>
        /// <param name="second">Segundo elo à ser comparado.</param>
        /// <returns>Verdadeiro caso o primeiro elo seja menor que o segundo e Falso caso contrário.</returns>
        public static bool operator <(ChainLink first, ChainLink second) => first.CompareTo(second) == -1;

        /// <summary>
        /// Comparação de maioridade e equalidade entre dois elos de blockchain.
        /// </summary>
        /// <param name="first">Primeiro elo à ser comparado.</param>
        /// <param name="second">Segundo elo à ser comparado.</param>
        /// <returns>Verdadeiro caso o primeiro elo seja maior ou igual ao segundo e Falso caso contrário.</returns>
        public static bool operator >=(ChainLink first, ChainLink second) => first.CompareTo(second) >= 0;

        /// <summary>
        /// Comparação de minoridade e equalidade entre dois elos de blockchain.
        /// </summary>
        /// <param name="first">Primeiro elo à ser comparado.</param>
        /// <param name="second">Segundo elo à ser comparado.</param>
        /// <returns>Verdadeiro caso o primeiro elo seja menor ou igual ao segundo e Falso caso contrário.</returns>
        public static bool operator <=(ChainLink first, ChainLink second) => first.CompareTo(second) <= 0;

        /// <summary>
        /// Método que efetua o cálculo de hash do elo de blockchain.
        /// </summary>
        /// <returns></returns>
        public string CalculateHash()
        {
            var data = $"{this.BlockchainId}-{this.Timestamp}-{this.PreviousHash ?? string.Empty}-{this.Data}";
            var hex = data.ComputeHash(HashAlgorithmName.SHA512).ToHex();
            return hex;
        }

        /// <summary>
        /// Método de comparação entre este e outro elo do blockchain.
        /// </summary>
        /// <param name="other">Outro elo do blockchain.</param>
        /// <returns></returns>
        public int CompareTo(ChainLink other)
        {
            if (this.Index == other.Index)
            {
                return 0;
            }
            else if (this.Index > other.Index)
            {
                return 1;
            }

            return -1;
        }

        /// <summary>
        /// Método de comparação de equalidade entre dois elos do blockchain.
        /// </summary>
        /// <param name="other">Outro elo do blockchain.</param>
        /// <returns>Verdadeiro caso os elos sejam iguais e falso caso contrário.</returns>
        public bool Equals(ChainLink other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }

        /// <summary>
        /// Método de comparação de equalidade entre este elo e um hash.
        /// </summary>
        /// <param name="other">Hash de blockchain.</param>
        /// <returns>Verdadeiro caso os elos sejam iguais e falso caso contrário.</returns>
        public bool Equals(string other)
        {
            return this.Hash == other;
        }

        /// <summary>
        /// Método de comparação de equalidade entre este elo e um objeto.
        /// </summary>
        /// <param name="obj">Objeto à ser comparado.</param>
        /// <returns>Verdadeiro caso os elos sejam iguais e falso caso contrário.</returns>
        public override bool Equals(object obj)
        {
            if (obj is ChainLink)
            {
                return this.Equals((ChainLink)obj);
            }

            return false;
        }

        /// <summary>
        /// Método que calcula o hashCode desta instância.
        /// </summary>
        /// <returns>HashCode da instância dessa classe.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Index, this.Timestamp, this.Hash, this.Data);
        }

        /// <summary>
        /// Método que retorna a representação string desta instância.
        /// </summary>
        /// <returns>Representação string da instância desta classe.</returns>
        public override string ToString()
        {
            return this.Hash;
        }
    }
}
