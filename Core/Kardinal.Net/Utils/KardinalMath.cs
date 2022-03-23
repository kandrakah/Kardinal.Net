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

using System.Linq;

namespace Kardinal.Net
{
    /// <summary>
    /// Métodos de auxílio matemático.
    /// </summary>
    public static class KardinalMath
    {
        /// <summary>
        /// Método que faz a soma de valores.
        /// </summary>
        /// <param name="values">Valores à serem somados.</param>
        /// <returns>Resultado da soma dos valores.</returns>
        public static decimal Sum(params decimal[] values)
        {
            var result = 0M;

            foreach (var value in values)
            {
                result += value;
            }
            return result;
        }

        /// <summary>
        /// Método que faz a soma de valores.
        /// </summary>
        /// <param name="values">Valores à serem somados.</param>
        /// <returns>Resultado da soma dos valores.</returns>
        public static int Sum(params int[] values)
        {
            var result = 0;

            foreach (var value in values)
            {
                result += value;
            }
            return result;
        }

        /// <summary>
        /// Método que faz a soma de valores.
        /// </summary>
        /// <param name="values">Valores à serem somados.</param>
        /// <returns>Resultado da soma dos valores.</returns>
        public static double Sum(params double[] values)
        {
            var result = 0D;

            foreach (var value in values)
            {
                result += value;
            }
            return result;
        }

        /// <summary>
        /// Método que faz a soma de valores.
        /// </summary>
        /// <param name="values">Valores à serem somados.</param>
        /// <returns>Resultado da soma dos valores.</returns>
        public static float Sum(params float[] values)
        {
            var result = 0F;

            foreach (var value in values)
            {
                result += value;
            }
            return result;
        }

        /// <summary>
        /// Método que retorna o maior valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Maior valor dentre os valores informados.</returns>
        public static decimal Max(params decimal[] values)
        {
            return values != null ? values.Max() : 0M;
        }

        /// <summary>
        /// Método que retorna o menor valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Maior valor dentre os valores informados.</returns>
        public static int Max(params int[] values)
        {
            return values != null ? values.Max() : 0;
        }

        /// <summary>
        /// Método que retorna o menor valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Maior valor dentre os valores informados.</returns>
        public static double Max(params double[] values)
        {
            return values != null ? values.Max() : 0D;
        }

        /// <summary>
        /// Método que retorna o menor valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Maior valor dentre os valores informados.</returns>
        public static float Max(params float[] values)
        {
            return values != null ? values.Max() : 0F;
        }

        /// <summary>
        /// Método que retorna o menor valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Menor valor dentre os valores informados.</returns>
        public static decimal Min(params decimal[] values)
        {
            return values != null ? values.Min() : 0M;
        }

        /// <summary>
        /// Método que retorna o menor valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Menor valor dentre os valores informados.</returns>
        public static double Min(params int[] values)
        {
            return values != null ? values.Min() : 0;
        }

        /// <summary>
        /// Método que retorna o menor valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Menor valor dentre os valores informados.</returns>
        public static double Min(params double[] values)
        {
            return values != null ? values.Min() : 0D;
        }

        /// <summary>
        /// Método que retorna o menor valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Menor valor dentre os valores informados.</returns>
        public static float Min(params float[] values)
        {
            return values != null ? values.Min() : 0F;
        }

        /// <summary>
        /// Método que retorna o valor médio dentre os 
        /// valores informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Valor médio entre os valores informados.</returns>
        public static decimal Average(params decimal[] values)
        {
            return values != null ? values.Average() : 0M;
        }

        /// <summary>
        /// Método que retorna o valor médio dentre os 
        /// valores informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Valor médio entre os valores informados.</returns>
        public static double Average(params int[] values)
        {
            return values != null ? values.Average() : 0;
        }

        /// <summary>
        /// Método que retorna o valor médio dentre os 
        /// valores informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Valor médio entre os valores informados.</returns>
        public static double Average(params double[] values)
        {
            return values != null ? values.Average() : 0D;
        }

        /// <summary>
        /// Método que retorna o valor médio dentre os 
        /// valores informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Valor médio entre os valores informados.</returns>
        public static float Average(params float[] values)
        {
            return values != null ? values.Average() : 0F;
        }

        /// <summary>
        /// Classe estática para cálculos geométricos.
        /// </summary>
        public static class Geometry
        {
            /// <summary>
            /// Classe estática para cálculos de quadrados.
            /// </summary>
            public static class Square
            {
                /// <summary>
                /// Método que calcula a área de um quadrado.
                /// </summary>
                /// <param name="side">Medida do lado do quadrado.</param>
                /// <returns>Área do quadrado.</returns>
                public static decimal Area(decimal side)
                {
                    return Area(side, side);
                }

                /// <summary>
                /// Método que calcula a área de um quadrado.
                /// </summary>
                /// <param name="width">Medida da largura do quadrado.</param>
                /// <param name="length">Medida do comprimento do quadrado.</param>
                /// <returns>Área do quadrado.</returns>
                public static decimal Area(decimal width, decimal length)
                {
                    return width * length;
                }

                /// <summary>
                /// Método que calcula a área de um quadrado.
                /// </summary>
                /// <param name="side">Medida do lado do quadrado.</param>
                /// <returns>Área do quadrado.</returns>
                public static int Area(int side)
                {
                    return Area(side, side);
                }

                /// <summary>
                /// Método que calcula a área de um quadrado.
                /// </summary>
                /// <param name="width">Medida da largura do quadrado.</param>
                /// <param name="length">Medida do comprimento do quadrado.</param>
                /// <returns>Área do quadrado.</returns>
                public static int Area(int width, int length)
                {
                    return width * length;
                }

                /// <summary>
                /// Método que calcula a área de um quadrado.
                /// </summary>
                /// <param name="side">Medida do lado do quadrado.</param>
                /// <returns>Área do quadrado.</returns>
                public static double Area(double side)
                {
                    return Area(side, side);
                }

                /// <summary>
                /// Método que calcula a área de um quadrado.
                /// </summary>
                /// <param name="width">Medida da largura do quadrado.</param>
                /// <param name="length">Medida do comprimento do quadrado.</param>
                /// <returns>Área do quadrado.</returns>
                public static double Area(double width, double length)
                {
                    return width * length;
                }

                /// <summary>
                /// Método que calcula a área de um quadrado.
                /// </summary>
                /// <param name="side">Medida do lado do quadrado.</param>
                /// <returns>Área do quadrado.</returns>
                public static float Area(float side)
                {
                    return Area(side, side);
                }

                /// <summary>
                /// Método que calcula a área de um quadrado.
                /// </summary>
                /// <param name="width">Medida da largura do quadrado.</param>
                /// <param name="length">Medida do comprimento do quadrado.</param>
                /// <returns>Área do quadrado.</returns>
                public static float Area(float width, float length)
                {
                    return width * length;
                }
            }
        }
    }
}
