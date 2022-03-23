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

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Classe estática para nomes de claims padrões.
    /// </summary>
    public static class DefaultClaims
    {
        /// <summary>
        /// Identificador do usuário.
        /// </summary>
        public static string SUB => "sub";

        /// <summary>
        /// Identificador do cliente do usuário.
        /// </summary>
        public static string CLIENT_ID => "client_id";

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public static string NAME => "name";

        /// <summary>
        /// Nome de família do usuário.
        /// </summary>
        public static string FAMILY_NAME => "family_name";

        /// <summary>
        /// Nome atribuído ao usuário.
        /// </summary>
        public static string GIVEN_NAME => "given_name";

        /// <summary>
        /// Sobrenome do usuário.
        /// </summary>
        public static string MIDDLE_NAME => "middle_name";

        /// <summary>
        /// Apelido do usuário.
        /// </summary>
        public static string NICKNAME => "nickname";

        /// <summary>
        /// Nome de usuário preferencial.
        /// </summary>
        public static string PREFERED_USERNAME => "prefered_username";

        /// <summary>
        /// Perfil do usuário.
        /// </summary>
        public static string PROFILE => "profile";

        /// <summary>
        /// Imagem do usuário.
        /// </summary>
        public static string PICTURE => "picture";

        /// <summary>
        /// Website do usuário.
        /// </summary>
        public static string WEBSITE => "website";

        /// <summary>
        /// Gênero do usuário.
        /// </summary>
        public static string GENDER => "gender";

        /// <summary>
        /// Data de nascimento do usuário.
        /// </summary>
        public static string BIRTHDATE => "birthdate";

        /// <summary>
        /// Informações de zona do usuário.
        /// </summary>
        public static string ZONEINFO => "zoneinfo";

        /// <summary>
        /// Localização do usuário.
        /// </summary>
        public static string LOCALE => "locale";

        /// <summary>
        /// Data de atualização do usuário.
        /// </summary>
        public static string UPDATED_AT => "updated_at";

        /// <summary>
        /// Nome de usuário.
        /// </summary>
        public static string USERNAME => "username";
    }
}
