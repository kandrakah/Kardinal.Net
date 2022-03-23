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
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Extensões para <see cref="PropertyBuilder"/>
    /// </summary>
    public static class PropertyBuilderExtensions
    {
        /// <summary>
        /// Extensão que adiciona a camada de proteção da propriedade, criptografando-a na base de dados
        /// e revertendo a criptografia quando a propriedade é solicitada pelo sistema.
        /// </summary>
        /// <typeparam name="TProperty">Tipo da propriedade.</typeparam>
        /// <param name="builder">Objeto referenciado.</param>
        /// <param name="provider">Provedor de proteção de dados de atributos.</param>
        /// <param name="purpose">Finalidade a ser atribuída ao <see cref="IDataProtector"/> responsável pelo processo de proteção das informações.</param>
        /// <param name="subPurposes">Finalidade secundárias a serem atribuídas ao <see cref="IDataProtector"/> responsável pelo processo de proteção da informação.</param>
        /// <returns>Builder da propriedade.</returns>
        public static PropertyBuilder<TProperty> IsProtected<TProperty>(this PropertyBuilder<TProperty> builder, IDataProtectionProvider provider, string purpose = "default", params string[] subPurposes)
        {
            return builder.IsProtected<TProperty, InternalDataProtectionFailSafeProvider>(provider, purpose, subPurposes);
        }

        /// <summary>
        /// Extensão que adiciona a camada de proteção da propriedade, criptografando-a na base de dados
        /// e revertendo a criptografia quando a propriedade é solicitada pelo sistema.
        /// </summary>
        /// <typeparam name="TProperty">Tipo da propriedade.</typeparam>
        /// <typeparam name="TFailSafeProvider">Tipo do provedor de failsafe.</typeparam>
        /// <param name="builder">Objeto referenciado.</param>
        /// <param name="provider">Provedor de proteção de dados de atributos.</param>
        /// <param name="purpose">Finalidade a ser atribuída ao <see cref="IDataProtector"/> responsável pelo processo de proteção das informações.</param>
        /// <param name="subPurposes">Finalidade secundárias a serem atribuídas ao <see cref="IDataProtector"/> responsável pelo processo de proteção da informação.</param>
        /// <returns>Builder da propriedade.</returns>
        public static PropertyBuilder<TProperty> IsProtected<TProperty, TFailSafeProvider>(this PropertyBuilder<TProperty> builder, IDataProtectionProvider provider, string purpose = "default", params string[] subPurposes) where TFailSafeProvider : IDataProtectionFailSafeProvider, new()
        {
            var failSafeProvider = Activator.CreateInstance<TFailSafeProvider>();
            return builder.IsProtected(provider, failSafeProvider, purpose, subPurposes);
        }

        /// <summary>
        /// Extensão que adiciona a camada de proteção da propriedade, criptografando-a na base de dados
        /// e revertendo a criptografia quando a propriedade é solicitada pelo sistema.
        /// </summary>
        /// <typeparam name="TProperty">Tipo da propriedade.</typeparam>
        /// <param name="builder">Objeto referenciado.</param>
        /// <param name="provider">Provedor de proteção de dados de atributos.</param>
        /// <param name="failSafeProvider">Instância do provedor de failsafe.</param>
        /// <param name="purpose">Finalidade a ser atribuída ao <see cref="IDataProtector"/> responsável pelo processo de proteção das informações.</param>
        /// <param name="subPurposes">Finalidade secundárias a serem atribuídas ao <see cref="IDataProtector"/> responsável pelo processo de proteção da informação.</param>
        /// <returns>Builder da propriedade.</returns>
        public static PropertyBuilder<TProperty> IsProtected<TProperty>(this PropertyBuilder<TProperty> builder, IDataProtectionProvider provider, IDataProtectionFailSafeProvider failSafeProvider, string purpose = "default", params string[] subPurposes)
        {
            if (typeof(TProperty) != typeof(string))
            {
                var converterType = typeof(ProtectedDataConverter<>).MakeGenericType(typeof(TProperty));
                var converter = Activator.CreateInstance(converterType, provider, failSafeProvider, purpose, subPurposes) as ValueConverter;
                builder.HasConversion(converter);
            }
            else
            {
                var converter = new ProtectedDataConverter(provider, failSafeProvider, purpose, subPurposes);
                builder.HasConversion(converter);
            }
            return builder;
        }
    }
}
