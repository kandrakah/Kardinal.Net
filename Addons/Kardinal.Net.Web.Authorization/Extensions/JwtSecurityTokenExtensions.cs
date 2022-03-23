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

using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Kardinal.Net.Web.Authorization
{
    /// <summary>
    /// Extensões para <see cref="JwtSecurityToken"/>
    /// </summary>
    public static class JwtSecurityTokenExtensions
    {
        /// <summary>
        /// Extensão que faz a validação de um token.
        /// </summary>
        /// <param name="token">Objeto referenciado</param>
        /// <param name="options">Configurações de autoridade</param>
        /// <returns>Resultado da validação do token</returns>
        public static async Task<KardinalTokenValidationResult> ValidateAsync(this JwtSecurityToken token, KardinalIdentityOptions options)
        {
            KardinalTokenValidationResult result;
            try
            {
                var tokenDecoder = new JwtSecurityTokenHandler();

                var parameters = await options.GetTokenParametesAsync(token);
                var principal = tokenDecoder.ValidateToken(token.RawData, parameters, out SecurityToken validatedToken);

                result = new KardinalTokenValidationResult(TokenValidationStatus.Valid)
                {
                    Principal = principal,
                    SecurityToken = validatedToken
                };
            }
            catch (SecurityTokenExpiredException ex)
            {
                result = new KardinalTokenValidationResult(TokenValidationStatus.Expired, ex.Message);
            }
            catch (SecurityTokenInvalidAudienceException ex)
            {
                result = new KardinalTokenValidationResult(TokenValidationStatus.InvalidAudience, ex.Message);
            }
            catch (SecurityTokenInvalidLifetimeException ex)
            {
                result = new KardinalTokenValidationResult(TokenValidationStatus.InvalidLifetime, ex.Message);
            }
            catch (SecurityTokenInvalidSignatureException ex)
            {
                result = new KardinalTokenValidationResult(TokenValidationStatus.InvalidSignature, ex.Message);
            }
            catch (SecurityTokenNoExpirationException ex)
            {
                result = new KardinalTokenValidationResult(TokenValidationStatus.NoExpiration, ex.Message);
            }
            catch (SecurityTokenNotYetValidException ex)
            {
                result = new KardinalTokenValidationResult(TokenValidationStatus.NotYetValid, ex.Message);
            }
            catch (SecurityTokenReplayAddFailedException ex)
            {
                result = new KardinalTokenValidationResult(TokenValidationStatus.ReplayAdd, ex.Message);
            }
            catch (SecurityTokenReplayDetectedException ex)
            {
                result = new KardinalTokenValidationResult(TokenValidationStatus.ReplayDetected, ex.Message);
            }
            catch (Exception ex)
            {
                result = new KardinalTokenValidationResult(TokenValidationStatus.Error, ex.Message);
            }

            return result;
        }
    }
    }
