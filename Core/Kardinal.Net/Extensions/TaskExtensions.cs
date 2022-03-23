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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Kardinal.Net
{
    /// <summary>
    /// Extensões para <see cref="Task"/>
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Extensão para obter resultado assíncrono de uma
        /// task.
        /// </summary>
        /// <param name="task">Objeto referenciado.</param>
        /// <param name="onSuccess">Ação executada quando a task é concluida com sucesso.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Objeto referenciado.</returns>
        public static Task Result([NotNull] this Task task, [NotNull] Action onSuccess, CancellationToken cancellationToken = default)
        {
            return task.ContinueWith(x =>
            {
                if (x.IsCompletedSuccessfully)
                {
                    onSuccess.Invoke();
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Extensão para obter resultado assíncrono de uma
        /// task.
        /// </summary>
        /// <typeparam name="T">Tipo do resultado da Task.</typeparam>
        /// <param name="task">Objeto referenciado.</param>
        /// <param name="onSuccess">Ação executada quando a task é concluida com sucesso.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Objeto referenciado.</returns>
        public static Task Result<T>([NotNull] this Task<T> task, [NotNull] Action<T> onSuccess, CancellationToken cancellationToken = default)
        {
            return task.ContinueWith(x =>
            {
                if (x.IsCompletedSuccessfully)
                {
                    onSuccess.Invoke(x.Result);
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Extensão para obter resultado assíncrono de uma
        /// task.
        /// </summary>
        /// <param name="task">Objeto referenciado.</param>
        /// <param name="onSuccess">Ação executada quando a task é concluida com sucesso.</param>
        /// <param name="onFail">Ação executada quando a task é concluída com falha.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Objeto referenciado.</returns>
        public static Task Result([NotNull] this Task task, [NotNull] Action onSuccess, [NotNull] Action<Exception> onFail, CancellationToken cancellationToken = default)
        {
            return task.ContinueWith(x =>
            {
                if (x.IsCompletedSuccessfully)
                {
                    onSuccess.Invoke();
                }
                else if (x.IsFaulted)
                {
                    onFail.Invoke(x.Exception);
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Extensão para obter resultado assíncrono de uma
        /// task.
        /// </summary>
        /// <typeparam name="T">Tipo do resultado da Task.</typeparam>
        /// <param name="task">Objeto referenciado.</param>
        /// <param name="onSuccess">Ação executada quando a task é concluida com sucesso.</param>
        /// <param name="onFail">Ação executada quando a task é concluída com falha.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Objeto referenciado.</returns>
        public static Task Result<T>([NotNull] this Task<T> task, [NotNull] Action<T> onSuccess, [NotNull] Action<Exception> onFail, CancellationToken cancellationToken = default)
        {
            return task.ContinueWith(x =>
            {
                if (x.IsCompletedSuccessfully)
                {
                    onSuccess.Invoke(x.Result);
                }
                else if (x.IsFaulted)
                {
                    onFail.Invoke(x.Exception);
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Extensão para obter resultado assíncrono de uma
        /// task.
        /// </summary>
        /// <param name="task">Objeto referenciado.</param>
        /// <param name="onSuccess">Ação executada quando a task é concluida com sucesso.</param>
        /// <param name="onFail">Ação executada quando a task é concluída com falha.</param>
        /// <param name="onCancel">Ação executada quando a task é cancelada.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Objeto referenciado.</returns>
        public static Task Result([NotNull] this Task task, [NotNull] Action onSuccess, [NotNull] Action<Exception> onFail, [NotNull] Action<TaskStatus> onCancel, CancellationToken cancellationToken = default)
        {
            return task.ContinueWith(x =>
            {
                if (x.IsCompletedSuccessfully)
                {
                    onSuccess?.Invoke();
                }
                else if (x.IsFaulted)
                {
                    onFail?.Invoke(x.Exception);
                }
                else if (x.IsCanceled)
                {
                    onCancel?.Invoke(x.Status);
                }

            }, cancellationToken);
        }

        /// <summary>
        /// Extensão para obter resultado assíncrono de uma
        /// task.
        /// </summary>
        /// <typeparam name="T">Tipo do resultado da Task.</typeparam>
        /// <param name="task">Objeto referenciado.</param>
        /// <param name="onSuccess">Ação executada quando a task é concluida com sucesso.</param>
        /// <param name="onFail">Ação executada quando a task é concluída com falha.</param>
        /// <param name="onCancel">Ação executada quando a task é cancelada.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Objeto referenciado.</returns>
        public static Task Result<T>([NotNull] this Task<T> task, [NotNull] Action<T> onSuccess, [NotNull] Action<Exception> onFail, [NotNull] Action<TaskStatus> onCancel, CancellationToken cancellationToken = default)
        {
            return task.ContinueWith(x =>
            {
                if (x.IsCompletedSuccessfully)
                {
                    onSuccess.Invoke(x.Result);
                }
                else if (x.IsFaulted)
                {
                    onFail.Invoke(x.Exception);
                }
                else if (x.IsCanceled)
                {
                    onCancel.Invoke(x.Status);
                }

            }, cancellationToken);
        }
    }
}
