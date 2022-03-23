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

using System.Xml.Serialization;

namespace Kardinal.Net.Blockchain
{
    /// <summary>
    /// Classe de modelo de um elo de blockchain.
    /// </summary>
    [XmlRoot("ChainLink")]
    [XmlType("SimpleChainLink")]
    public class SimpleChainLinkSerializedModel
    {
        /// <summary>
        /// Índice do elo.
        /// </summary>
        [XmlElement(Type = typeof(int))]
        public int Index { get; set; }

        /// <summary>
        /// Data de criação do blockchain.
        /// </summary>
        [XmlElement(Type = typeof(long))]
        public long Timestamp { get; set; }

        /// <summary>
        /// Dados do blockchain.
        /// </summary>
        [XmlElement(Type = typeof(byte[]))]
        public byte[] Data { get; set; }

        /// <summary>
        /// Hash do elo anterior do blockchain.
        /// </summary>
        [XmlElement(Type = typeof(string))]
        public string PreviousHash { get; set; }

        /// <summary>
        /// Hash do elo do blockchain.
        /// </summary>
        [XmlElement(Type = typeof(string))]
        public string Hash { get; set; }        

        /// <summary>
        /// Método que retorna a representação string desta instância.
        /// </summary>
        /// <returns>Representação string da instância desta classe.</returns>
        public override string ToString()
        {
            return $"[{this.Index}]{this.Hash}";
        }
    }
}
