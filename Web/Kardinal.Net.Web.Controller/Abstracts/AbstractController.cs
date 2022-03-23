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

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe base para todos os controllers do sistema.
    /// </summary>
    public class AbstractController : ControllerBase, IController
    {
        /// <summary>
        /// Instância do serviço de logs.
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Instância do provedor de serviços. Veja <see cref="IServiceProvider"/> para mais detalhes.
        /// </summary>
        protected readonly IServiceProvider _provider;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// /// <param name="provider">Instância do provedor de serviços.</param>
        protected AbstractController(IServiceProvider provider)
        {
            this._provider = provider;

            var loggerFactory = this._provider.GetKardinalService<ILoggerFactory>();
            this._logger = loggerFactory.CreateLogger(this.GetType().Name);

        }

        /// <summary>
        /// Método que obtém um serviço através da injeção de dependências.
        /// </summary>
        /// <typeparam name="T">Interface do serviço desejado.</typeparam>
        /// <param name="replacementService">Instância de serviço de substituição caso o serviço solicitado não seja encontrado.</param>
        /// <returns>Instância do serviço desejado.</returns>
        protected T GetService<T>(T replacementService = default)
        {
            return this._provider.GetKardinalService(replacementService);
        }

        /// <summary>
        /// Método que obtém os dados do usuário atualmente autenticado.
        /// </summary>
        /// <returns>Instância do serviço contendo os dados do usuário. Veja <see cref="ICurrentUserService"/></returns>
        protected ICurrentUserService GetCurrentUser()
        {
            return this.GetService<ICurrentUserService>();
        }

        /// <summary>
        /// Método que obtém os dados da aplicação atual.
        /// </summary>
        /// <returns>Instância do serviço de informações da aplicação.</returns>
        protected IApplicationInfoService GetApplicationInfo()
        {
            return this.GetService<IApplicationInfoService>();
        }

        /// <summary>
        /// Método que responte à requisição com um erro.
        /// </summary>
        /// <param name="exception">exceção à ser respondida à requisição.</param>
        /// <returns>Resposta à ser devolvida ao requerente da requisição. <see cref="ActionResult"/></returns>
        protected ActionResult Error(Exception exception)
        {
            try
            {
                var service = this.GetService<IConfiguration>();
                if (exception is ServiceException)
                {
                    var ex = exception as ServiceException;
                    this._logger.LogError(ex, ex.StatusMessage);
                    return this.Error(ex.StatusCode, ex.Message, null);
                }                              
                else
                {
                    this._logger.LogError(exception, exception.Message);
                    return this.Error(HttpStatusCode.InternalServerError, "Ocorreu uma falha interna.");
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"Falha ao processar exceção: {ex.Message}");
                return this.StatusCode((int)HttpStatusCode.InternalServerError, "Ocorreu uma falha interna.");
            }
        }

        /// <summary>
        /// Método que responte à requisição com um erro.
        /// </summary>
        /// <param name="statusCode">Código de erro da exceção.</param>
        /// <returns>Resposta à ser devolvida ao requerente da requisição. <see cref="ActionResult"/></returns>
        protected ActionResult Error(HttpStatusCode statusCode)
        {
            return this.StatusCode((int)statusCode, statusCode.GetDescription());
        }

        /// <summary>
        /// Método que responte à requisição com um erro.
        /// </summary>
        /// <param name="statusCode">Código de erro da exceção.</param>
        /// <param name="message">Mensagem à ser apresentada na exceção.</param>
        /// <returns>Resposta à ser devolvida ao requerente da requisição. <see cref="ActionResult"/></returns>
        protected ActionResult Error(HttpStatusCode statusCode, string message)
        {
            return this.Error((int)statusCode, message);
        }

        /// <summary>
        /// Método que responte à requisição com um erro.
        /// </summary>
        /// <param name="statusCode">Código de erro da exceção.</param>
        /// <param name="message">Mensagem à ser apresentada na exceção.</param>
        /// <returns>Resposta à ser devolvida ao requerente da requisição. <see cref="ActionResult"/></returns>
        protected ActionResult Error(int statusCode, string message)
        {
            var data = new StatusCodeModel()
            {
                Status = statusCode,
                Title = message
            };

            this._logger.LogError($"{statusCode}:{message}");
            return this.StatusCode(statusCode, data);
        }

        /// <summary>
        /// Método que responte à requisição com um erro.
        /// </summary>
        /// <param name="statusCode">Código de erro da exceção.</param>
        /// <param name="obj">Detalhes da exceção.</param>
        /// <returns>Resposta à ser devolvida ao requerente da requisição. <see cref="ActionResult"/></returns>
        protected ActionResult Error(HttpStatusCode statusCode, object obj)
        {
            return this.Error((int)statusCode, obj);
        }

        /// <summary>
        /// Método que responte à requisição com um erro.
        /// </summary>
        /// <param name="statusCode">Código de erro da exceção.</param>
        /// <param name="obj">Detalhes da exceção.</param>
        /// <returns>Resposta à ser devolvida ao requerente da requisição. <see cref="ActionResult"/></returns>
        protected ActionResult Error(int statusCode, object obj)
        {
            var data = new StatusCodeModel()
            {
                Status = statusCode,
                Title = StatusCodeUtils.GetDescription(statusCode),
                Details = obj
            };

            this._logger.LogError($"{statusCode}:{data.Title}");
            return this.StatusCode(statusCode, data);
        }

        /// <summary>
        /// Método que responte à requisição com um erro.
        /// </summary>
        /// <param name="statusCode">Código de erro da exceção.</param>
        /// <param name="message">Mensagem à ser apresentada na exceção.</param>
        /// <param name="obj">Detalhes da exceção.</param>
        /// <returns>Resposta à ser devolvida ao requerente da requisição. <see cref="ActionResult"/></returns>
        protected ActionResult Error(HttpStatusCode statusCode, string message, object obj)
        {
            return this.Error((int)statusCode, message, obj);
        }

        /// <summary>
        /// Método que responte à requisição com um erro.
        /// </summary>
        /// <param name="statusCode">Código de erro da exceção.</param>
        /// <param name="message">Mensagem à ser apresentada na exceção.</param>
        /// <param name="obj">Detalhes da exceção.</param>
        /// <returns>Resposta à ser devolvida ao requerente da requisição. <see cref="ActionResult"/></returns>
        protected ActionResult Error(int statusCode, string message, object obj)
        {
            var data = new StatusCodeModel()
            {
                Status = statusCode,
                Title = message,
                Details = obj
            };

            this._logger.LogError($"{statusCode}:{data.Title}");
            return this.StatusCode(statusCode, data);
        }

        /// <summary>
        /// Método que responte à requisição com um erro.
        /// </summary>
        /// <param name="statusCode">Código de erro da exceção.</param>
        /// <param name="exception">exceção à ser respondida à requisição.</param>
        /// <param name="obj">Objeto à ser apresentado na exceção.</param>
        /// <returns>Resposta à ser devolvida ao requerente da requisição. <see cref="ActionResult"/></returns>
        protected ActionResult Error(HttpStatusCode statusCode, Exception exception, object obj)
        {
            return this.Error((int)statusCode, exception, obj);
        }

        /// <summary>
        /// Método que responte à requisição com um erro.
        /// </summary>
        /// <param name="statusCode">Código de erro da exceção.</param>
        /// <param name="exception">exceção à ser respondida à requisição.</param>
        /// <param name="obj">Objeto à ser apresentado na exceção.</param>
        /// <returns>Resposta à ser devolvida ao requerente da requisição. <see cref="ActionResult"/></returns>
        protected ActionResult Error(int statusCode, Exception exception, object obj)
        {
            var data = new StatusCodeModel()
            {
                Status = statusCode,
                Title = StatusCodeUtils.GetDescription(statusCode),
                Details = obj
            };

            this._logger.LogError($"{statusCode}:{StatusCodeUtils.GetDescription(statusCode)}", exception);
            return this.StatusCode(statusCode, data);
        }
    }
}
