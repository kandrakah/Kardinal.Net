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

using Kardinal.Net.Localization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe de extensões para <see cref="decimal"/>.
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// Extensão para transformar o valor decimal em sua representação monetária.
        /// </summary>
        /// <param name="value">Objeto referenciado</param>
        /// <returns>Representação monetária do valor decimal</returns>
        public static string AsCurrency(this decimal value)
        {
            return string.Format(Resource.Culture, "{0:C}", value);
        }

        /// <summary>
        /// Extensão para transformar o valor decimal em sua representação monetária.
        /// </summary>
        /// <param name="value">Este valor</param>
        /// <returns>Representação monetária do valor decimal</returns>
        public static string AsCurrency(this decimal? value)
        {
            return value != null ? string.Format(Resource.Culture, "{0:C}", value) : string.Format(Resource.Culture, "{0:C}", 0);
        }

        /// <summary>
        /// Extensão para transformar o valor decimal em sua representação monetária.
        /// </summary>
        /// <param name="value">Este valor</param>
        /// <param name="culture">Informações de idioma à serem usados na conversão</param>
        /// <returns>Representação monetária do valor decimal</returns>
        public static string AsCurrency(this decimal value, [NotNull] CultureInfo culture)
        {
            return string.Format(culture, "{0:C}", value);
        }

        /// <summary>
        /// Extensão para transformar o valor decimal em sua representação monetária.
        /// </summary>
        /// <param name="value">Este valor.</param>
        /// <param name="culture">Informações de idioma à serem usados na conversão.</param>
        /// <returns>Representação monetária do valor decimal.</returns>
        public static string AsCurrency(this decimal? value, [NotNull] CultureInfo culture)
        {
            return value != null ? string.Format(culture, "{0:C}", value) : string.Format(culture, "{0:C}", 0);
        }

        /// <summary>
        /// Extensão para transformar o valor decimal em porcentagem.
        /// </summary>
        /// <param name="value">Este valor.</param>
        /// <param name="transform">Indica se o valor deve ser normalizado.</param>
        /// <returns>Representação em porcentagem do valor decimal.</returns>
        public static string AsPercentage(this decimal value, bool transform = false)
        {
            return transform ? $"{value * 100}%" : $"{value}%";
        }

        /// <summary>
        /// Extensão para transformar o valor decimal em porcentagem.
        /// </summary>
        /// <param name="value">Este valor.</param>
        /// <param name="transform">Indica se o valor deve ser normalizado.</param>
        /// <returns>Representação em porcentagem do valor decimal.</returns>
        public static string AsPercentage(this decimal? value, bool transform = false)
        {
            return value != null ? transform ? $"{value * 100}%" : $"{value}%" : "0%";
        }

        /// <summary>
        /// Extensão que efetua o cálculo para descobrir quanto o valor atual
        /// é em porcentagem de um valor total.
        /// </summary>
        /// <param name="value">Este valor.</param>
        /// <param name="total">Valor de referência total.</param>
        /// <param name="decimals">Quantidade de casas decimais desejadas.</param>
        /// <returns>Valor percentual resultante do cálculo.</returns>
        public static decimal AsPercentOf(this decimal value, decimal total, int decimals = 2)
        {
            var result = (value / total * 100);
            return Math.Round(result, decimals);
        }

        /// <summary>
        /// Extensão que efetua o cálculo para descobrir quanto informado é
        /// em porcentagem do valor atual.
        /// </summary>
        /// <param name="value">Este valor.</param>
        /// <param name="total">Valor de referência total.</param>
        /// <param name="decimals">Quantidade de casas decimais desejadas.</param>
        /// <returns>Valor percentual resultante do cálculo.</returns>
        public static decimal AsPercentFrom(this decimal total, decimal value, int decimals = 2)
        {
            return value.AsPercentOf(total, decimals);
        }
    }
}
