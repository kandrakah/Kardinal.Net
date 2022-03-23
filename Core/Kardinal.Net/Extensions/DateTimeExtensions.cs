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

using System;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe de extensões para <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Data Inicial de um DateTime.
        /// </summary>
        private static DateTime InitialDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

        /// <summary>
        /// Extensão que traz a diferença da data atual à data inicial válida.
        /// </summary>
        /// <param name="source">Objeto referenciado</param>
        /// <param name="field">Tipo de campo à ser retornado</param>
        /// <returns>Diferença entre as datas baseando-se no tipo de campo solicitado</returns>
        public static long Elapsed(this DateTime source, DateField field)
        {
            return source.Diference(InitialDate, field);
        }

        /// <summary>
        /// Extensão que verifica se uma data está entre duas datas específicas.
        /// </summary>
        /// <param name="source">Objeto referenciado</param>
        /// <param name="initialDate">Data inicial do período</param>
        /// <param name="finalDate">Data final do período</param>
        /// <returns>Verdadeiro caso a data esteja dentro do período e falso caso contrário</returns>
        public static bool IsBetween(this DateTime source, DateTime initialDate, DateTime finalDate)
        {
            return source >= initialDate && source <= finalDate;
        }

        /// <summary>
        /// Extensão que verifica se uma data é anterior à outra.
        /// </summary>
        /// <param name="source">Objeto referenciado</param>
        /// <param name="reference">Data de referência para comparação.</param>
        /// <returns>Verdadeiro caso a data atual seja anterior à data de referência e Falso caso contrário.</returns>
        public static bool IsBefore(this DateTime source, DateTime reference)
        {
            return source < reference;
        }

        /// <summary>
        /// Extensão que verifica se uma data é posterior à outra.
        /// </summary>
        /// <param name="source">Objeto referenciado</param>
        /// <param name="reference">Data de referência para comparação.</param>
        /// <returns>Verdadeiro caso a data atual seja posterior à data de referência e Falso caso contrário.</returns>
        public static bool IsAfter(this DateTime source, DateTime reference)
        {
            return source > reference;
        }

        /// <summary>
        /// Extensão que traz a diferença da data atual à uma data informada.
        /// </summary>
        /// <param name="source">Objeto referenciado</param>
        /// <param name="date">Data à ser comparada</param>
        /// <param name="field">Tipo de campo à ser retornado</param>
        /// <returns>Diferença entre as datas baseando-se no tipo de campo solicitado</returns>
        public static long Diference(this DateTime source, DateTime date, DateField field)
        {
            var timeSpan = source - date;

            var result = timeSpan.GetValue(field);

            if (result < 0)
            {
                result *= (-1);
            }
            return result;
        }
    }
}
