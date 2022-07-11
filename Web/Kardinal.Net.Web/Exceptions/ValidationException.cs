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

using Kardinal.Net.Web.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de exceção de validação.
    /// </summary>
    public class ValidationException : KardinalException
    {
        /// <summary>
        /// Coleção de validações contidas na exceção.
        /// </summary>
        public ICollection<ValidationModel> Validations { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        public ValidationException() : base(Resource.ERROR_VALIDATION)
        {
            this.Validations = new List<ValidationModel>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        public ValidationException(string message) : base(message)
        {
            this.Validations = new List<ValidationModel>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="validations">Mensagens de validação.</param>
        public ValidationException(params ValidationModel[] validations) : this()
        {
            foreach (var validation in validations)
            {
                this.Validations.Add(validation);
            }
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        /// <param name="validations">Mensagens de validação.</param>
        public ValidationException(string message, params ValidationModel[] validations) : this(message)
        {
            foreach (var validation in validations)
            {
                this.Validations.Add(validation);
            }
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        /// <param name="innerException">Exceção interior.</param>
        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
            this.Validations = new List<ValidationModel>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="info"> The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        public ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.Validations = new List<ValidationModel>();
        }

        /// <summary>
        /// Método que adiciona uma mensagem de validação à lista.
        /// </summary>
        /// <param name="field">Campo relacionado à validação.</param>
        /// <param name="message">Mensagem de validação.</param>
        public void Add(string field, string message)
        {
            this.Validations.Add(new ValidationModel()
            {
                Field = field,
                Message = message
            });
        }

        /// <summary>
        /// Método que indica se há mensagens de validação na exceção.
        /// </summary>
        /// <returns>Verdadeiro caso exista alguma mensagem de validação ou falso caso contrário.</returns>
        public bool Any()
        {
            return this.Validations.Any();
        }

        /// <summary>
        /// Método que lança a exceção caso haja alguma validação.
        /// </summary>
        public void Throw()
        {
            if (this.Any())
            {
                throw this;
            }
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return string.Join(string.Empty, this.Validations);
        }
    }
}
