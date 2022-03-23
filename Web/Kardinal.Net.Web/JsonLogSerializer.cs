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

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe interna de serialização de objetos para log.
    /// </summary>
    internal class JsonLogSerializer
    {
        private static readonly JsonSerializerSettings options = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        /// <summary>
        /// Construtor estático.
        /// </summary>
        static JsonLogSerializer()
        {
            options.Converters.Add(new StringEnumConverter());
            options = JsonConvert.DefaultSettings?.Invoke() ?? options;
        }

        /// <summary>
        /// Método para serialização de dados.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        internal static object Desserialize(string json)
        {
            return JsonConvert.DeserializeObject(json, typeof(object), options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        internal static T Desserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, options);
        }
    }
}
