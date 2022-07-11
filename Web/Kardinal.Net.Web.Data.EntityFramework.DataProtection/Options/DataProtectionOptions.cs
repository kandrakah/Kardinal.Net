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

using System.Security.Cryptography.X509Certificates;

namespace Kardinal.Net.Web.Data
{
    /// <summary>
    /// Configurações de proteção de dados.
    /// </summary>
    public class DataProtectionOptions
    {
        /// <summary>
        /// Nome da aplicação.
        /// </summary>
        public string ApplicationName { get; set; } = Constants.ApplicationName;

        /// <summary>
        /// Tipo de armazenamento das chaves de segurança. Veja <see cref="DataProtectionKeyStorageType"/>
        /// </summary>
        public DataProtectionKeyStorageType KeyStorage { get; set; } = DataProtectionKeyStorageType.FileSystem;

        /// <summary>
        /// Fonte do certificado de proteção de dados. Veja <see cref="DataProtectionCertificateSourceType"/>
        /// </summary>
        public DataProtectionCertificateSourceType Source { get; set; } = DataProtectionCertificateSourceType.None;

        /// <summary>
        /// Caminho do arquivo do certificado.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Senha da chave privada do certificado.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Nome do diretório de certificados.
        /// </summary>
        public StoreName StoreName { get; set; } = StoreName.My;

        /// <summary>
        /// Localização do diretório de certificados.
        /// </summary>
        public StoreLocation StoreLocation { get; set; } = StoreLocation.CurrentUser;

        /// <summary>
        /// Indica que somente certificados válidos devem ser considerados.
        /// </summary>
        public bool ValidOnly { get; set; }

        /// <summary>
        /// Impressão digital do certificado.
        /// </summary>
        public string Thumbprint { get; set; }
    }
}
