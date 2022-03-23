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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Implementação do serviço de execução de tarefas em segundo plano.
    /// </summary>
    public class BackgroundTaskHostedService : BackgroundService
    {
        /// <summary>
        /// Instância do serviço de log.
        /// </summary>
        private readonly ILogger<BackgroundTaskHostedService> _logger;

        /// <summary>
        /// Instância do serviço de escopo.
        /// </summary>
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// Instância do serviço de fila de tarefas em segundo plano.
        /// </summary>
        public readonly IBackgroundTaskQueue _taskQueue;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="taskQueue">Instância do serviço de fila de tarefas em segundo plano.</param>
        /// <param name="serviceScopeFactory">Instância do serviço de escopo.</param>
        /// <param name="logger">Instância do serviço de logs.</param>
        public BackgroundTaskHostedService(IBackgroundTaskQueue taskQueue, IServiceScopeFactory serviceScopeFactory, ILogger<BackgroundTaskHostedService> logger)
        {
            this._logger = logger;
            this._serviceScopeFactory = serviceScopeFactory;
            this._taskQueue = taskQueue;
        }

        /// <summary>
        /// Método executado quando o serviço de segundo plano é inicializado.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        public override Task StartAsync(CancellationToken cancellationToken = default)
        {
            this._logger.LogDebug(Resource.DEBUG_BACKGROUND_TASK_HOST_START);
            return base.StartAsync(cancellationToken);
        }

        /// <summary>
        /// Método executado quando uma solicitação de parada do serviço é feita.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        public override async Task StopAsync(CancellationToken cancellationToken = default)
        {
            this._logger.LogDebug(Resource.DEBUG_BACKGROUND_TASK_HOST_STOP);
            await base.StopAsync(cancellationToken);
        }

        /// <summary>
        /// Método executado quando o serviço de background é executado.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            await this.BackgroundProcessing(cancellationToken);
        }

        private async Task BackgroundProcessing(CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var task = await _taskQueue.DequeueAsync(cancellationToken);

                try
                {
                    await task(this._serviceScopeFactory, cancellationToken);
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, Resource.ERROR_BACKGROUND_TASK_FAIL, nameof(task), ex.Message);
                }
            }
        }
    }
}
