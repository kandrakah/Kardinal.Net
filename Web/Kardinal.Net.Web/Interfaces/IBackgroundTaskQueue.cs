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

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Interface do serviço de fila de tarefas em segundo plano.
    /// </summary>
    public interface IBackgroundTaskQueue
    {
        /// <summary>
        /// Método para registro de uma nova tarefa à ser executada.
        /// </summary>
        /// <param name="task">Informações da tarefa à ser executada.</param>    
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>    
        ValueTask QueueBackgroundTaskAsync(Func<IServiceScopeFactory, CancellationToken, ValueTask> task, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para obtenção de de uma das tarefas à serem executadas.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Tarefa à ser executada.</returns>
        ValueTask<Func<IServiceScopeFactory, CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken = default);
    }
}
