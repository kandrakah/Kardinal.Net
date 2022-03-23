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

namespace Kardinal.Net
{
    /// <summary>
    /// Classe de definição de valor de parâmetro de string.
    /// </summary>
    public class StringParameter
    {
        /// <summary>
        /// Chave da tradução.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Valor da tradução.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        public StringParameter()
        {

        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="key">Chave da tradução.</param>
        /// <param name="value">Valor da tradução.</param>
        public StringParameter(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Método estático para definição de valor de tradução.
        /// </summary>
        /// <param name="key">Chave da tradução.</param>
        /// <param name="value">Valor da tradução.</param>
        /// <returns>Instância de <see cref="StringParameter"/>.</returns>
        public static StringParameter Set(string key, object value)
        {
            return new StringParameter(key, value);
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return $"[{this.Key}]{this.Value}";
        }
    }
}
