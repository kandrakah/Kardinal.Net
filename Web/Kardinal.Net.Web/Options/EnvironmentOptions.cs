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

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de configurações de ambiente.
    /// </summary>
    public class EnvironmentOptions : AbstractOptions
    {
        /// <summary>
        /// Tipo de ambiente.
        /// </summary>
        public EnvironmentType Environment { get; set; } = EnvironmentType.Development;

        /// <summary>
        /// Método construtor.
        /// </summary>
        public EnvironmentOptions() : base("Environment")
        {

        }

        /// <summary>
        /// Método que verifica se o ambiente é de desenvolvimento.
        /// </summary>
        /// <returns>Verdadeiro caso o ambiente seja o de desenvolvimento e falso caso contrário.</returns>
        public bool IsDevelopment()
        {
            return this.Environment == EnvironmentType.Development;
        }

        /// <summary>
        /// Método que verifica se o ambiente é de homologação.
        /// </summary>
        /// <returns>Verdadeiro caso o ambiente seja o de homologação e falso caso contrário.</returns>
        public bool IsStaging()
        {
            return this.Environment == EnvironmentType.Staging;
        }

        /// <summary>
        /// Método que verifica se o ambiente é de produção.
        /// </summary>
        /// <returns>Verdadeiro caso o ambiente seja o de homologação e falso caso produção.</returns>
        public bool IsProduction()
        {
            return this.Environment == EnvironmentType.Production;
        }
    }
}
