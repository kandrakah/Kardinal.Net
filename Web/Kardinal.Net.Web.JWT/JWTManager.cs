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

using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Cryptography;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe de gerenciamento de token.
    /// </summary>
    public class JWTManager
    {
        /// <summary>
        /// Handler de tokens.
        /// </summary>
        private readonly JwtSecurityTokenHandler _handler;

        /// <summary>
        /// Nome do emissor do token.
        /// </summary>
        private readonly string _issuer;

        /// <summary>
        /// Enumeração de audiências válidas.
        /// </summary>
        private readonly IEnumerable<string> _validAudiences;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="issuer">Nome do emissor do token.</param>
        /// <param name="validAudiences">Enumeração de audiências válidas.</param>
        public JWTManager(string issuer, IEnumerable<string> validAudiences)
        {
            this._handler = new JwtSecurityTokenHandler();
            this._issuer = issuer;
            this._validAudiences = validAudiences;
        }

        /// <summary>
        /// Método que verifica se o token pode ser lido.
        /// </summary>
        /// <param name="token">Token à ser validado.</param>
        /// <returns>Verdadeiro caso o token possa ser lido e falso caso contrário.</returns>
        public bool CanReadToken(string token)
        {
            var result = _handler.CanReadToken(token);
            return result;
        }

        /// <summary>
        /// Método que faz a leitura do token.
        /// </summary>
        /// <param name="token">Token à ser lido.</param>
        /// <returns><see cref="SecurityToken"/> contendo os dados do token informado.</returns>
        public SecurityToken ReadToken(string token)
        {
            var result = _handler.ReadToken(token);
            return result;
        }

        /// <summary>
        /// Método que faz a leitura do token.
        /// </summary>
        /// <param name="token">Token à ser lido.</param>
        /// <returns><see cref="JwtSecurityToken"/> contendo os dados do token informado.</returns>
        public JwtSecurityToken ReadJwtToken(string token)
        {
            var result = _handler.ReadJwtToken(token);
            return result;
        }

        /// <summary>
        /// Método que faz a criação de um token.
        /// </summary>
        /// <param name="audience">Audiência do token.</param>
        /// <param name="expiration">Validade, em anos, do token.</param>
        /// <param name="claims">Atributos do token.</param>
        /// <param name="keyParameters">Parâmetros da chave de segurança do token. Veja <see cref="RSAParameters"/></param>
        /// <param name="algorithm">Algoritmo utilizado na geração da assinatura do token. Veja <see cref="SecurityAlgorithms"/></param>
        /// <returns>Token JWT gerado.</returns>
        public string CreateToken(string audience, int expiration, IDictionary<string, object> claims, RSAParameters keyParameters, string algorithm = SecurityAlgorithms.RsaSha512Signature)
        {
            var now = DateTime.UtcNow;
            var descriptor = new SecurityTokenDescriptor();
            descriptor.Issuer = this._issuer;
            descriptor.Audience = audience;
            descriptor.NotBefore = now;
            descriptor.Expires = now.AddYears(expiration);
            descriptor.SigningCredentials = this.CreateSigningCredentials(this.GetSecurityKey(keyParameters), algorithm);
            descriptor.Claims = claims;

            var token = _handler.CreateEncodedJwt(descriptor);
            return token;
        }

        /// <summary>
        /// Método que faz a criação de um token.
        /// </summary>
        /// <param name="audience">Audiência do token.</param>
        /// <param name="expiration">Data de validade do token.</param>
        /// <param name="claims">Atributos do token.</param>
        /// <param name="keyParameters">Parâmetros da chave de segurança do token. Veja <see cref="RSAParameters"/></param>
        /// <param name="algorithm">Algoritmo utilizado na geração da assinatura do token. Veja <see cref="SecurityAlgorithms"/></param>
        /// <returns>Token JWT gerado.</returns>
        public string CreateToken(string audience, DateTime expiration, IDictionary<string, object> claims, RSAParameters keyParameters, string algorithm = SecurityAlgorithms.RsaSha512Signature)
        {
            var now = DateTime.UtcNow;
            var descriptor = new SecurityTokenDescriptor();
            descriptor.IssuedAt = now;
            descriptor.Issuer = _issuer;
            descriptor.Audience = audience;
            descriptor.NotBefore = now;
            descriptor.Expires = expiration;
            descriptor.SigningCredentials = this.CreateSigningCredentials(this.GetSecurityKey(keyParameters), algorithm);
            descriptor.Claims = claims;

            var token = _handler.CreateEncodedJwt(descriptor);
            return token;
        }

        /// <summary>
        /// Método que faz a validação do token.
        /// </summary>
        /// <param name="token">Token que será validado.</param>
        /// <param name="keyParameters">Parâmetros da chave de segurança do token. Veja <see cref="RSAParameters"/></param>
        public SecurityToken ValidateToken(string token, RSAParameters keyParameters)
        {
            if (!_handler.CanReadToken(token))
            {
                throw new InvalidCredentialException("O token informado não pôde ser lido!");
            }

            _handler.ReadJwtToken(token);

            try
            {
                _handler.ValidateToken(token, this.GetValidationParameters(keyParameters), out SecurityToken securityToken);
                return securityToken;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que gera um objeto <see cref="SecurityKey"/> com base nos parâmetros
        /// da chave de segurança.
        /// </summary>
        /// <param name="keyParameters">Parâmetros da chave de segurança. Veja <see cref="RSAParameters"/></param>
        /// <returns><see cref="SecurityKey"/> contendo os dados da chave de segurança.</returns>
        private SecurityKey GetSecurityKey(RSAParameters keyParameters)
        {
            var key = new RsaSecurityKey(keyParameters);
            return key;
        }

        /// <summary>
        /// Método que cria as credenciais de assinatura do token.
        /// </summary>
        /// <param name="securityKey">Dados da chave de segurança. Veja <see cref="SecurityKey"/></param>
        /// <param name="algorithm">Algoritmo utilizado na geração da assinatura do token. Veja <see cref="SecurityAlgorithms"/></param>
        /// <returns>Dados das credenciais de asinatura do token. Veja <see cref="SigningCredentials"/></returns>
        private SigningCredentials CreateSigningCredentials(SecurityKey securityKey, string algorithm = SecurityAlgorithms.RsaSha512Signature)
        {
            var credentials = new SigningCredentials(securityKey, algorithm);
            return credentials;
        }

        /// <summary>
        /// Método que obtém os parâmetros de validação de token.
        /// </summary>
        /// <param name="keyParameters">Parâmetros da chave de segurança. Veja <see cref="RSAParameters"/></param>
        /// <returns><see cref="TokenValidationParameters"/> contendo os parâmetros de validação.</returns>
        private TokenValidationParameters GetValidationParameters(RSAParameters keyParameters)
        {
            var parameters = new TokenValidationParameters();
            parameters.ValidateIssuer = true;
            parameters.ValidateLifetime = true;
            parameters.ValidateAudience = true;
            parameters.ValidateIssuerSigningKey = true;
            parameters.ValidIssuer = this._issuer;
            parameters.ValidAudiences = this._validAudiences;
            parameters.IssuerSigningKey = this.GetSecurityKey(keyParameters);

            return parameters;
        }
    }
}
