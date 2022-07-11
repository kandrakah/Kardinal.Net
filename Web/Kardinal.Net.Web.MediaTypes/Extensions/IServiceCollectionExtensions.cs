﻿/*
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

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Extensões para <see cref="IServiceCollection"/>
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Extensão para adição do serviço de detecção de tipos de mídia.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddMediaTypesService(this IServiceCollection services)
        {
            services.AddSingleton<IMediaTypeService, MediaTypeService>();
            return services;
        }
    }
}
