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

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe de logger de failSafe.
    /// </summary>
    /// <typeparam name="TCategoryName">Categoria do logger.</typeparam>
    public class DefaultLogger<TCategoryName> : DefaultLogger, ILogger<TCategoryName>
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        public DefaultLogger() : base()
        {
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="enabledLevels">Dicionário de níveis de log habilitados.</param>
        public DefaultLogger(params LogLevel[] enabledLevels) : base(enabledLevels)
        {

        }
    }

    /// <summary>
    /// Classe de logger de failSafe.
    /// </summary>
    public class DefaultLogger : ILogger, IDisposable
    {
        /// <summary>
        /// Coleção de níveis de logs habilitados.
        /// </summary>
        protected readonly ICollection<LogLevel> _enabledLevels;

        /// <summary>
        /// Método construtor.
        /// </summary>
        public DefaultLogger()
        {
            this._enabledLevels = new HashSet<LogLevel>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="enabledLevels">Dicionário de níveis de log habilitados.</param>
        public DefaultLogger(params LogLevel[] enabledLevels)
        {
            this._enabledLevels = new HashSet<LogLevel>();
            foreach (var level in enabledLevels)
            {
                if (!this._enabledLevels.Contains(level))
                {
                    this._enabledLevels.Add(level);
                }
            }
        }

        /// <summary>
        /// Begins a logical operation scope.
        /// </summary>
        /// <typeparam name="TState">The type of the state to begin scope for.</typeparam>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns> A disposable object that ends the logical operation scope on dispose.</returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        /// <summary>
        /// Checks if the given logLevel is enabled.
        /// </summary>
        /// <param name="logLevel">level to be checked.</param>
        /// <returns>true if enabled; false otherwise.</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return this._enabledLevels.Any(x => x == logLevel);
        }

        /// <summary>
        /// Writes a log entry.
        /// </summary>
        /// <typeparam name="TState">The type of the object to be written.</typeparam>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">Function to create a System.String message of the state and exception.</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var now = DateTime.Now;
            Console.WriteLine($"[CONSOLE][{now.ToString("HH:mm:ss")}: {logLevel,-12}] {formatter(state, exception)}");
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this._enabledLevels.Clear();
        }
    }
}
