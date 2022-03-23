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
    /// Classe de extensões para <see cref="TimeSpan"/>.
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Extensão que converte o <see cref="TimeSpan"/> em um valor relativo ao campo
        /// de data indicado.
        /// </summary>
        /// <param name="timeSpan">TimeSpan de diferenciação entre datas</param>
        /// <param name="field">Tipo de campo desejado</param>
        /// <returns>Contagem de diferença entre as datas com base no tipo de campo informado</returns>
        public static long GetValue(this TimeSpan timeSpan, DateField field)
        {
            switch (field)
            {
                case DateField.Days:
                    return timeSpan.Days;
                case DateField.Hours:
                    return timeSpan.Hours;
                case DateField.Minutes:
                    return timeSpan.Minutes;
                case DateField.Seconds:
                    return timeSpan.Seconds;
                case DateField.Milissecounds:
                    return timeSpan.Milliseconds;
                default:
                    return timeSpan.Ticks;
            }
        }
    }
}
