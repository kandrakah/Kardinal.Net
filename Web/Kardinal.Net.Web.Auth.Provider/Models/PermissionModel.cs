using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kardinal.Net.Web.Auth
{
    /// <summary>
    /// Classe de modelo de permissão.
    /// </summary>
    public class PermissionModel
    {
        /// <summary>
        /// Id da permissão.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id do grupo da permissão.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// Chave única da permissão.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Nome apresentável da permissão.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Descrição da permissão.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}
