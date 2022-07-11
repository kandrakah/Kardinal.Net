/*
Kardinal.Net
Copyright(C) 2022 Marcelo O.Mendes


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

using Kardinal.Net.Web.Authorization.Localization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Kardinal.Net.Web.Authorization
{
    /// <summary>
    /// Implementação do serviço de geração de senhas.
    /// </summary>
    public class PasswordGeneratorService : IPasswordGeneratorService
    {
        private static readonly char[] DEFAULT_LOWERCASE_CHARACTERS = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private static readonly char[] DEFAULT_UPPERCASE_CHARACTERS = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static readonly char[] DEFAULT_NUMBERS = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        private static readonly char[] DEFAULT_SPECIAL_CHARACTERS = { '@', '$', '%', '#', '&', '!', '?' };

        private readonly PasswordOptions _options;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="provider">Instância do provedor de serviços.</param>
        public PasswordGeneratorService(IServiceProvider provider)
        {
            var options = provider.GetOptions<IdentityOptions>();
            if (options != null && options.Password != null)
            {
                this._options = options.Password;
            }
            else
            {
                this._options = new PasswordOptions();
            }
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="options">Configurações de senha.</param>
        private PasswordGeneratorService([NotNull] PasswordOptions options)
        {
            this._options = options;
        }

        /// <summary>
        /// Instância do serviço de criação de senhas.
        /// </summary>
        public static PasswordGeneratorService Instance => new PasswordGeneratorService(new PasswordOptions());

        /// <summary>
        /// Método que gera uma nova senha.
        /// </summary>
        /// <param name="length">Comprimento da senha à ser gerada.</param>
        /// <param name="exclusions">Caracteres à serem excluídos da senha à ser gerada.</param>
        /// <returns>Senha gerada</returns>
        public string Generate(int length = 15, params char[] exclusions)
        {
            if (length < this._options.RequiredLength)
            {
                throw new ArgumentException($"{Resource.ERROR_INVALID_PASSWORD_LENGHT} [{this._options.RequiredLength}]");
            }

            string result;
            do
            {
                var password = new char[length];
                var charactersSet = this.GetCharacterSet(exclusions);
                var shuffledChars = charactersSet.Shuffle();
                var shuffledCharactersSet = string.Join(null, shuffledChars);

                for (var characterPosition = 0; characterPosition < length; characterPosition++)
                {
                    password[characterPosition] = shuffledCharactersSet[GetRandomNumberInRange(0, shuffledCharactersSet.Length - 1)];

                    var moreThanTwoIdenticalInARow = characterPosition > this._options.RequiredUniqueChars && password[characterPosition] == password[characterPosition - 1] && password[characterPosition - 1] == password[characterPosition - 2];

                    if (moreThanTwoIdenticalInARow)
                    {
                        characterPosition--;
                    }
                }

                result = string.Join(string.Empty, password);
            } while (!this.IsValid(result));

            return result;
        }

        /// <summary>
        /// Método que faz a validação da senha gerada junto aos critérios de senha do serviço.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Verdadeiro caso a senha seja váldia e falso caso contrário.</returns>
        private bool IsValid(string password)
        {
            var regexLowercase = @"[a-z]";
            var regexUppercase = @"[A-Z]";
            var regexNumeric = @"[\d]";

            var lowerCaseIsValid = !this._options.RequireLowercase || this._options.RequireLowercase && Regex.IsMatch(password, regexLowercase);

            var upperCaseIsValid = !this._options.RequireUppercase || this._options.RequireUppercase && Regex.IsMatch(password, regexUppercase);

            var numericIsValid = !this._options.RequireDigit || this._options.RequireDigit && Regex.IsMatch(password, regexNumeric);

            var specialIsValid = !this._options.RequireNonAlphanumeric;

            if (this._options.RequireNonAlphanumeric)
            {
                var passwordArray = password.ToCharArray();
                specialIsValid = DEFAULT_SPECIAL_CHARACTERS.Any(x => passwordArray.Contains(x));
            }

            return lowerCaseIsValid && upperCaseIsValid && numericIsValid && specialIsValid && password.Length >= this._options.RequiredLength;
        }

        /// <summary>
        /// Método que obtém o character set com base nos critérios de senha do serviço.
        /// </summary>
        /// <returns></returns>
        private string GetCharacterSet(params char[] exclusions)
        {
            var result = string.Empty;
            if (this._options.RequireLowercase)
            {
                result += string.Join(string.Empty, DEFAULT_LOWERCASE_CHARACTERS.Except(exclusions));
            }

            if (this._options.RequireUppercase)
            {
                result += string.Join(string.Empty, DEFAULT_UPPERCASE_CHARACTERS.Except(exclusions));
            }

            if (this._options.RequireDigit)
            {
                result += string.Join(string.Empty, DEFAULT_NUMBERS.Except(exclusions));
            }

            if (this._options.RequireNonAlphanumeric)
            {
                result += string.Join(string.Empty, DEFAULT_SPECIAL_CHARACTERS.Except(exclusions));
            }

            return result;
        }

        /// <summary>
        /// Método que obtém um número randônico dentro de uma faixa estabelecida.
        /// </summary>
        /// <param name="min">Valor mínimo da faixa.</param>
        /// <param name="max">Valor máximo da faixa.</param>
        /// <returns>Valor aleatório dentro da faixa estabelecida.</returns>
        private int GetRandomNumberInRange(int min, int max)
        {
            var provider = RandomNumberGenerator.Create();
            var data = new byte[sizeof(int)];
            provider.GetBytes(data);
            var rnd = BitConverter.ToInt32(data, 0);
            return (int)Math.Floor((double)(min + Math.Abs(rnd % (max - min))));
        }
    }
}
