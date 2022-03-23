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

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Generic;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de extensões para <see cref="ILoggingBuilder"/>.
    /// </summary>
    public static class ILoggingBuilderExtensions
    {
        /// <summary>
        /// Extensão para adição do Serilog ao serviço.
        /// </summary>
        /// <param name="loggingBuilder">Instância de <see cref="ILoggingBuilder"/></param>
        /// <param name="config">Instância de <see cref="IConfiguration"/></param>
        /// <param name="sectionName">Nome da sessão de configurações</param>
        /// <returns>Instância de <see cref="ILoggingBuilder"/> informado via parâmetro <paramref name="loggingBuilder"/></returns>
        public static ILoggingBuilder AddSerilog(this ILoggingBuilder loggingBuilder, IConfiguration config, string sectionName = ConfigurationLoggerConfigurationExtensions.DefaultSectionName)
        {
            var loggerConfig = new LoggerConfiguration();
            if (config.GetSection(sectionName).Exists())
            {
                loggerConfig.ReadFrom.Configuration(config, sectionName);
            }
            else
            {
                var settings = new Dictionary<string, string>()
                {
                    { "write-to:Console", ""},
                    { "using:File", "Serilog.Sinks.Console" },
                };
                loggerConfig.ReadFrom.KeyValuePairs(settings);
            }

            var logger = loggerConfig.CreateLogger();
            loggingBuilder.ClearProviders();
            return loggingBuilder.AddSerilog(logger, true);
        }
    }
}
