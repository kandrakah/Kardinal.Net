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
using System.Diagnostics.CodeAnalysis;

namespace Kardinal.Net.Blockchain
{
    /// <summary>
    /// Interface de serialização de dados blockchain.
    /// </summary>
    public interface ISimpleBlockchainDataSerializer
    {
        /// <summary>
        /// Método que verifica se o objeto pode ser serializado.
        /// </summary>
        /// <typeparam name="T">ipo do objeto à ser serializado.</typeparam>
        /// <param name="obj">Objeto à ser serializado.</param>
        /// <returns>Verdadeiro caso o objeto possa ser serializado e falso caso contrário.</returns>
        bool CanSerialize<T>(T obj);

        /// <summary>
        /// Método que verifica se o objeto pode ser desserializado.
        /// </summary>
        /// <typeparam name="T">ipo do objeto à ser serializado.</typeparam>
        /// <param name="data">Dados à serem desserializados.</param>
        /// <returns>Verdadeiro caso o objeto possa ser desserializado e falso caso contrário.</returns>
        bool CanDeserialize<T>(string data);

        /// <summary>
        /// Método para serialização de dados.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto à ser serializado.</typeparam>
        /// <param name="obj">Objeto à ser serializado.</param>
        /// <returns>Valor serializado do objeto.</returns>
        string SerializeChainLink<T>(T obj);

        /// <summary>
        /// Método para desserialziação de dados.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto à ser desserialziado.</typeparam>
        /// <param name="data">Dados do objeto à ser desserializado.</param>
        /// <returns>Objeto desserializado.</returns>
        T DesserializeChainLink<T>(string data);

        /// <summary>
        /// Método que efetua a serialização do blockchain.
        /// </summary>
        /// <param name="blockchain">Blockchain à ser serializado.</param>
        /// <param name="chainLinks">Enumeração de elos do blockchain.</param>
        /// <returns>Forma serializada do blockchain.</returns>
        string Serialize([NotNull] SimpleBlockchain blockchain, [NotNull] IEnumerable<ChainLink> chainLinks);

        /// <summary>
        /// Método que efetua a serialização do blockchain.
        /// </summary>        
        /// <returns>Forma serializada do blockchain.</returns>
        SimpleBlockchain Deserialize([NotNull] string data);
    }
}
