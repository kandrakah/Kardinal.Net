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

using System.Collections.Generic;
using System.Linq;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Implementação do construtor de validações.
    /// </summary>
    public sealed class ValidationBuilder : IValidationBuilder
    {
        /// <summary>
        /// Coleção de validações.
        /// </summary>
        private readonly ICollection<ValidationModel> _validations;

        private readonly string _message;

        /// <summary>
        /// Método construtor.
        /// </summary>
        public ValidationBuilder()
        {
            _validations = new HashSet<ValidationModel>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem geral de validação.</param>
        public ValidationBuilder(string message) : this()
        {
            this._message = message;
        }

        /// <summary>
        /// Método para adição de validação.
        /// </summary>
        /// <param name="field">Chave de identificação da validação.</param>
        /// <param name="message">Mensagem da validação.</param>
        public IValidationBuilder Add(string field, string message)
        {
            var validation = new ValidationModel()
            {
                Field = field,
                Message = message
            };

            this._validations.Add(validation);

            return this;
        }

        /// <summary>
        /// Método para lançamento das exceções de validação.
        /// O disparo ocorre gerando uma exceção do tipo <see cref="ValidationException"/>
        /// caso haja validações no construtor.
        /// </summary>
        public void Throw()
        {
            if (_validations.Any())
            {
                if (string.IsNullOrEmpty(this._message))
                {
                    throw new ValidationException(this._validations.ToArray());
                }
                else
                {
                    throw new ValidationException(this._message, this._validations.ToArray());
                }                
            }
        }

        /// <summary>
        /// Método para liberação de recursos usados pelo serviço.
        /// </summary>
        public void Dispose()
        {
            this._validations.Clear();
        }
    }
}
