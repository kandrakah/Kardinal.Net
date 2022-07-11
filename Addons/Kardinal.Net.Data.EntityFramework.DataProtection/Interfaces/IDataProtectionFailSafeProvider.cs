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

namespace Kardinal.Net.Data
{
    /// <summary>
    /// Interface do provedor de failsafe para proteção de dados.
    /// </summary>
    public interface IDataProtectionFailSafeProvider
    {
        /// <summary>
        /// Método de tratamento de falha ao proteger informação.
        /// </summary>
        /// <typeparam name="T">Tipo do valor à ser protegido.</typeparam>
        /// <param name="ex">Exceção gerada ao tentar proteger a informação.</param>
        /// <returns>Resultado do tratamento da exceção.</returns>
        string OnProtectFail<T>(Exception ex);

        /// <summary>
        /// Método de tratamento de falha ao desproteger informação.
        /// </summary>
        /// <typeparam name="T">Tipo do valor à ser desprotegido.</typeparam>
        /// <param name="ex">Exceção gerada ao tentar desproteger a informação.</param>
        /// <returns>Resultado do tratamento da exceção.</returns>
        T OnUnprotectFail<T>(Exception ex);
    }
}
