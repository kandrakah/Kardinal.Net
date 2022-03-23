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

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe de opções de certificado do host.
    /// </summary>
    public class HostCertificateOptions
    {
        /// <summary>
        /// Indica que é necessário usar o certificado.
        /// </summary>
        public bool UseCertificate { get; set; } = false;

        /// <summary>
        /// Fonte do certificado.
        /// </summary>
        public CertificateSource Storage { get; set; } = CertificateSource.FileSystem;

        /// <summary>
        /// Diretório do arquivo de certificado.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Senha do certificado.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Localização do repositório de certificados.
        /// </summary>
        public StoreLocation StoreLocation { get; set; } = StoreLocation.CurrentUser;

        /// <summary>
        /// Nome do repositório de certificados.
        /// </summary>
        public StoreName StoreName { get; set; } = StoreName.My;

        /// <summary>
        /// Impressão digital do certificado.
        /// </summary>
        public string Thumbprint { get; set; }

        /// <summary>
        /// Indica que devem ser carregados apenas certificados válidos.
        /// </summary>
        public bool ValidOnly { get; set; }
    }
}
