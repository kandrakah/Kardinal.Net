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

using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de conversão de dados que devem ser protegidos na base de dados com criptografia.
    /// </summary>
    internal sealed class ProtectedDataConverter : ValueConverter<string, string>
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="protectionProvider">Instância do provedor de proteção.</param>
        /// <param name="failSafeProvider">Instância do provedor de failsafe.</param>
        /// <param name="purpose">Finalidade a ser atribuída ao <see cref="IDataProtector"/> responsável pelo processo de proteção da informação.</param>
        /// <param name="subPurposes">Finalidade secundárias a serem atribuídas ao <see cref="IDataProtector"/> responsável pelo processo de proteção da informação.</param>
        internal ProtectedDataConverter([NotNull] IDataProtectionProvider protectionProvider, [NotNull] IDataProtectionFailSafeProvider failSafeProvider, string purpose, params string[] subPurposes) : base(s => Protect(protectionProvider, failSafeProvider, purpose, subPurposes, s), s => Unprotect(protectionProvider, failSafeProvider, purpose, subPurposes, s), default)
        {
        }

        /// <summary>
        /// Método que efetua a proteção do valor.
        /// </summary>
        /// <param name="protectionProvider">Instância do provedor de proteção.</param>
        /// <param name="failSafeProvider">Instância do provedor de failsafe.</param>
        /// <param name="purpose">Propósito da proteção.</param>
        /// <param name="subPurposes">Subpropósitos da proteção.</param>
        /// <param name="value">Valor à ser protegido.</param>
        /// <returns>Valor protegido.</returns>
        private static string Protect([NotNull] IDataProtectionProvider protectionProvider, [NotNull] IDataProtectionFailSafeProvider failSafeProvider, [NotNull] string purpose, [NotNull] string[] subPurposes, string value)
        {
            try
            {
                var protector = protectionProvider.CreateProtector(purpose, subPurposes);
                var result = protector.Protect(value);
                return result;
            }
            catch (Exception ex)
            {
                return failSafeProvider.OnProtectFail<string>(ex);
            }
        }

        /// <summary>
        /// Método que efetua a desproteção do valor.
        /// </summary>
        /// <param name="protectionProvider">Instância do provedor de proteção.</param>
        /// <param name="failSafeProvider">Instância do provedor de failsafe.</param>
        /// <param name="purpose">Propósito da proteção.</param>
        /// <param name="subPurposes">Subpropósitos da proteção.</param>
        /// <param name="value">Valor à ser protegido.</param>
        /// <returns>Valor desprotegido.</returns>
        private static string Unprotect([NotNull] IDataProtectionProvider protectionProvider, [NotNull] IDataProtectionFailSafeProvider failSafeProvider, [NotNull] string purpose, [NotNull] string[] subPurposes, [NotNull] string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return value;
                }
                var protector = protectionProvider.CreateProtector(purpose, subPurposes);
                var result = protector.Unprotect(value);
                return result;
            }
            catch (Exception ex)
            {
                return failSafeProvider.OnUnprotectFail<string>(ex);
            }
        }
    }

    /// <summary>
    /// Classe de conversão de dados que devem ser protegidos na base de dados com criptografia.
    /// </summary>
    /// <typeparam name="T">Tipo do atributo à ser protegido.</typeparam>
    internal sealed class ProtectedDataConverter<T> : ValueConverter<T, string>
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="protectionProvider">Instância do provedor de proteção.</param>
        /// <param name="failSafeProvider">Instância do provedor de failsafe.</param>
        /// <param name="purpose">Finalidade a ser atribuída ao <see cref="IDataProtector"/> responsável pelo processo de proteção da informação.</param>
        /// <param name="subPurposes">Finalidade secundárias a serem atribuídas ao <see cref="IDataProtector"/> responsável pelo processo de proteção da informação.</param>
        internal ProtectedDataConverter([NotNull] IDataProtectionProvider protectionProvider, [NotNull] IDataProtectionFailSafeProvider failSafeProvider, [NotNull] string purpose, params string[] subPurposes) : base(s => Protect(protectionProvider, failSafeProvider, purpose, subPurposes, s), s => Unprotect(protectionProvider, failSafeProvider, purpose, subPurposes, s), default)
        {
        }

        /// <summary>
        /// Método que efetua a proteção do valor.
        /// </summary>
        /// <param name="protectionProvider">Instância do provedor de proteção.</param>
        /// <param name="failSafeProvider">Instância do provedor de failsafe.</param>
        /// <param name="purpose">Propósito da proteção.</param>
        /// <param name="subPurposes">Subpropósitos da proteção.</param>
        /// <param name="value">Valor à ser protegido.</param>
        /// <returns>Valor protegido.</returns>
        private static string Protect([NotNull] IDataProtectionProvider protectionProvider, [NotNull] IDataProtectionFailSafeProvider failSafeProvider, [NotNull] string purpose, [NotNull] string[] subPurposes, [NotNull] T value)
        {
            try
            {
                var data = JsonSerializer.Serialize(value);
                var protector = protectionProvider.CreateProtector(purpose, subPurposes);
                var result = protector.Protect(data);
                return result;
            }
            catch (Exception ex)
            {
                return failSafeProvider.OnProtectFail<T>(ex);
            }
        }

        /// <summary>
        /// Método que efetua a desproteção do valor.
        /// </summary>
        /// <param name="protectionProvider">Instância do provedor de proteção.</param>
        /// <param name="failSafeProvider">Instância do provedor de failsafe.</param>
        /// <param name="purpose">Propósito da proteção.</param>
        /// <param name="subPurposes">Subpropósitos da proteção.</param>
        /// <param name="value">Valor à ser desprotegido.</param>
        /// <returns>Valor desprotegido.</returns>
        private static T Unprotect([NotNull] IDataProtectionProvider protectionProvider, [NotNull] IDataProtectionFailSafeProvider failSafeProvider, [NotNull] string purpose, [NotNull] string[] subPurposes, [NotNull] string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return default(T);
                }
                var protector = protectionProvider.CreateProtector(purpose, subPurposes);
                var json = protector.Unprotect(value);
                var result = JsonSerializer.Deserialize<T>(json);
                return result;
            }
            catch (Exception ex)
            {
                return failSafeProvider.OnUnprotectFail<T>(ex);
            }
        }
    }
}
