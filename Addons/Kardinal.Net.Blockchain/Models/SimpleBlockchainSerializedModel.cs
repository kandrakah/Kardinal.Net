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

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Kardinal.Net.Blockchain
{
    /// <summary>
    /// Classe de modelo para serialização de blockchain.
    /// </summary>
    [XmlRoot("Blockchain")]
    [XmlType("SimpleBlockchain")]
    public class SimpleBlockchainSerializedModel
    {
        /// <summary>
        /// Id do blockchain.
        /// </summary>
        [XmlElement(Type = typeof(string))]
        public string BlockchainId { get; set; }

        /// <summary>
        /// Contagens de elos do blockchain.
        /// </summary>
        [XmlElement(Type = typeof(int))]
        public int Count => this.ChainLinks != null ? this.ChainLinks.Count : 0;

        /// <summary>
        /// Índice inicial do blockchain.
        /// </summary>
        [XmlElement(Type = typeof(int))]
        public int FirstIndex { get; set; }

        /// <summary>
        /// Índice final do blockchain.
        /// </summary>
        [XmlElement(Type = typeof(int))]
        public int LastIndex { get; set; }

        /// <summary>
        /// Lista de elos do blockchain.
        /// </summary>
        [XmlArrayItem(Type = typeof(SimpleChainLinkSerializedModel))]
        public List<SimpleChainLinkSerializedModel> ChainLinks { get; set; }

        /// <summary>
        /// Método que retorna a representação string desta instância.
        /// </summary>
        /// <returns>Representação string da instância desta classe.</returns>
        public override string ToString()
        {
            return this.BlockchainId;
        }
    }
}
