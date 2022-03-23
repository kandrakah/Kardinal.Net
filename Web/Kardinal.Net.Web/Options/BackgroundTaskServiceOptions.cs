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

using System.Threading.Channels;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de configurações de serviços em segundo plano.
    /// </summary>
    public class BackgroundTaskServiceOptions : AbstractOptions
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        public BackgroundTaskServiceOptions() : base("BackgroundTasks")
        {

        }

        /// <summary>
        /// Capacidade de execução simultânea de tarefas em segundo plano.
        /// </summary>
        public int BackgrounTaskQueueCapacity { get; set; } = 1000;

        /// <summary>
        /// Indica a ação tomada quando a fila de execução simultânea está com sua capacidade esgotada.
        /// </summary>
        public BoundedChannelFullMode BackgroundTaskFullMode { get; set; } = BoundedChannelFullMode.Wait;
    }
}
