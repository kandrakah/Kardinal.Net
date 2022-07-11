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

using Microsoft.AspNetCore.Http;
using System;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de modelo do manipulador de endpoint.
    /// </summary>
    public class EndpointHandlerModel
    {
        /// <summary>
        /// Nome do manipulador.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Caminho do manipulador.
        /// </summary>
        public PathString Path { get; set; }

        /// <summary>
        /// Tipo do manipulador. Veja <see cref="Type"/>
        /// </summary>
        public Type Handler { get; set; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="name">Nome do manipulador.</param>
        /// <param name="path">Caminho do manipulador.</param>
        /// <param name="handlerType">Tipo do manipulador.</param>
        public EndpointHandlerModel(string name, string path, Type handlerType)
        {
            if (!Uri.IsWellFormedUriString(path, UriKind.Relative))
            {
                throw new KardinalException($"O caminho do endpoint [{name}] é inválido.");
            }
            Name = name;
            Path = path;
            Handler = handlerType;
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
