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
using System.Security.Cryptography;

namespace Kardinal.Net.Data
{
    /// <summary>
    /// Extensões para <see cref="EntityTypeBuilder"/>
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// Extensão que irá adicionar o sistema de criptografia de dados das propriedades da entidade marcados com o atributo <see cref="ProtectedAttribute"/>.
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade com dados à serem protegidos.</typeparam>
        /// <param name="builder">Objeto referenciado.</param>
        /// <param name="parameters">Parâmetros de chaves RSA utilizadas na criptografia.</param>
        /// <returns>Objeto referenciado.</returns>
        public static EntityTypeBuilder<TEntity> AddDataProtection<TEntity>(this EntityTypeBuilder<TEntity> builder, RSAParameters parameters) where TEntity : Entity
        {
            var provider = new DefaultDataProtectionProvider(parameters);
            return builder.AddDataProtection<TEntity, InternalDataProtectionFailSafeProvider>(provider);
        }

        /// <summary>
        /// Extensão que irá adicionar o sistema de criptografia de dados das propriedades da entidade marcados com o atributo <see cref="ProtectedAttribute"/>.
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade com dados à serem protegidos.</typeparam>
        /// <param name="builder">Objeto referenciado.</param>
        /// <param name="provider">Provedor de proteção de dados de atributos.</param>
        /// <param name="purpose">Finalidade a ser atribuída ao <see cref="IDataProtector"/> responsável pelo processo de proteção das informações.</param>
        /// <param name="subPurposes">Finalidade secundárias a serem atribuídas ao <see cref="IDataProtector"/> responsável pelo processo de proteção da informação.</param>
        /// <returns>Objeto referenciado.</returns>
        public static EntityTypeBuilder<TEntity> AddDataProtection<TEntity>(this EntityTypeBuilder<TEntity> builder, IDataProtectionProvider provider, string purpose = "default", params string[] subPurposes) where TEntity : Entity
        {
            return builder.AddDataProtection<TEntity, InternalDataProtectionFailSafeProvider>(provider, purpose, subPurposes);
        }

        /// <summary>
        /// Extensão que irá adicionar o sistema de criptografia de dados das propriedades da entidade marcados com o atributo <see cref="ProtectedAttribute"/>.
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade com dados à serem protegidos.</typeparam>
        /// <typeparam name="TFailSafeProvider">Tipo do provedor de failsafe.</typeparam>
        /// <param name="builder">Objeto referenciado.</param>
        /// <param name="provider">Provedor de proteção de dados de atributos.</param>
        /// <param name="purpose">Finalidade a ser atribuída ao <see cref="IDataProtector"/> responsável pelo processo de proteção das informações.</param>
        /// <param name="subPurposes">Finalidade secundárias a serem atribuídas ao <see cref="IDataProtector"/> responsável pelo processo de proteção da informação.</param>
        /// <returns>Objeto referenciado.</returns>
        public static EntityTypeBuilder<TEntity> AddDataProtection<TEntity, TFailSafeProvider>(this EntityTypeBuilder<TEntity> builder, IDataProtectionProvider provider, string purpose = "default", params string[] subPurposes) where TEntity : Entity where TFailSafeProvider : IDataProtectionFailSafeProvider, new()
        {
            var failSafeProvider = Activator.CreateInstance<TFailSafeProvider>();
            return builder.AddDataProtection(provider, failSafeProvider, purpose, subPurposes);
        }

        /// <summary>
        /// Extensão que irá adicionar o sistema de criptografia de dados das propriedades da entidade marcados com o atributo <see cref="ProtectedAttribute"/>.
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade com dados à serem protegidos.</typeparam>
        /// <param name="builder">Objeto referenciado.</param>
        /// <param name="provider">Provedor de proteção de dados de atributos.</param>
        /// <param name="failSafeProvider">Instância do provedor de failsafe.</param>
        /// <param name="purpose">Finalidade a ser atribuída ao <see cref="IDataProtector"/> responsável pelo processo de proteção das informações.</param>
        /// <param name="subPurposes">Finalidade secundárias a serem atribuídas ao <see cref="IDataProtector"/> responsável pelo processo de proteção da informação.</param>
        /// <returns>Objeto referenciado.</returns>
        public static EntityTypeBuilder<TEntity> AddDataProtection<TEntity>(this EntityTypeBuilder<TEntity> builder, IDataProtectionProvider provider, IDataProtectionFailSafeProvider failSafeProvider, string purpose = "default", params string[] subPurposes) where TEntity : Entity
        {
            var protectedProps = typeof(TEntity).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ProtectedAttribute)));
            foreach (var p in protectedProps)
            {
                if (p.PropertyType != typeof(string))
                {
                    var converterType = typeof(ProtectedDataConverter<>).MakeGenericType(p.PropertyType);
                    var converter = Activator.CreateInstance(converterType, provider, failSafeProvider, purpose, subPurposes) as ValueConverter;
                    builder.Property(p.PropertyType, p.Name).HasConversion(converter);
                }
                else
                {
                    var converter = new ProtectedDataConverter(provider, failSafeProvider, purpose, subPurposes);
                    builder.Property(typeof(string), p.Name).HasConversion(converter);
                }
            }
            return builder;
        }
    }
}
