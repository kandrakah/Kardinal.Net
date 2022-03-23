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

using Kardinal.Net.Localization;
using System;
using System.Text.RegularExpressions;

namespace Kardinal.Net
{
    /// <summary>
    /// Representa uma versão de arquivo.
    /// </summary>
    public struct KardinalVersion : IComparable, IComparable<KardinalVersion>, IComparable<string>, IEquatable<KardinalVersion>, IEquatable<string>
    {
        /// <summary>
        /// Versão principal.
        /// </summary>
        public int Major { get; private set; }

        /// <summary>
        /// Versão secundária.
        /// </summary>
        public int Minor { get; private set; }

        /// <summary>
        /// Versão de atualização.
        /// </summary>
        public int Update { get; private set; }

        /// <summary>
        /// Versão de revisão.
        /// </summary>
        public int Revision { get; private set; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="major">Versão principal</param>
        /// <param name="minor">Versão secundária</param>
        /// <param name="update">Versão de atualização</param>
        /// <param name="revision">Versão de revisão</param>
        internal KardinalVersion(int major = 0, int minor = 0, int update = 0, int revision = 0)
        {
            Major = major;
            Minor = minor;
            Update = update;
            Revision = revision;
        }

        /// <summary>
        /// Comparação de igualdade entre duas versões.
        /// </summary>
        /// <param name="first">Primeira versão à ser comparada.</param>
        /// <param name="second">Segunda versão à ser comparada.</param>
        /// <returns>Verdadeiro caso ambas as versões sejam iguais e Falso caso contrário.</returns>
        public static bool operator ==(KardinalVersion first, KardinalVersion second) => first.Equals(second);

        /// <summary>
        /// Comparação de diferença entre duas versões.
        /// </summary>
        /// <param name="first">Primeira versão à ser comparada.</param>
        /// <param name="second">Segunda versão à ser comparada.</param>
        /// <returns>Verdadeiro caso ambas as versões sejam diferentes e Falso caso contrário.</returns>
        public static bool operator !=(KardinalVersion first, KardinalVersion second) => !first.Equals(second);

        /// <summary>
        /// Comparação de maioridade entre duas versões.
        /// </summary>
        /// <param name="first">Primeira versão à ser comparada.</param>
        /// <param name="second">Segunda versão à ser comparada.</param>
        /// <returns>Verdadeiro caso a primeira versão seja maior que a segunda e Falso caso contrário.</returns>
        public static bool operator >(KardinalVersion first, KardinalVersion second) => first.Newer(second);

        /// <summary>
        /// Comparação de minoridade entre duas versões.
        /// </summary>
        /// <param name="first">Primeira versão à ser comparada.</param>
        /// <param name="second">Segunda versão à ser comparada.</param>
        /// <returns>Verdadeiro caso a primeira versão seja menor que a segunda e Falso caso contrário.</returns>
        public static bool operator <(KardinalVersion first, KardinalVersion second) => first.Older(second);

        /// <summary>
        /// Comparação de maioridade ou igualdade entre duas versões.
        /// </summary>
        /// <param name="first">Primeira versão à ser comparada.</param>
        /// <param name="second">Segunda versão à ser comparada.</param>
        /// <returns>Verdadeiro caso a primeira versão seja maior ou igual que a segunda e Falso caso contrário.</returns>
        public static bool operator >=(KardinalVersion first, KardinalVersion second) => first.Equals(second) || first.Newer(second);

        /// <summary>
        /// Comparação de minoridade ou igualdade entre duas versões.
        /// </summary>
        /// <param name="first">Primeira versão à ser comparada.</param>
        /// <param name="second">Segunda versão à ser comparada.</param>
        /// <returns>Verdadeiro caso a primeira versão seja menor ou igual que a segunda e Falso caso contrário.</returns>
        public static bool operator <=(KardinalVersion first, KardinalVersion second) => first.Equals(second) || first.Older(second);

        /// <summary>
        /// Comparação de igualdade entre duas versões.
        /// </summary>
        /// <param name="first">Primeira versão à ser comparada.</param>
        /// <param name="second">Segunda versão à ser comparada.</param>
        /// <returns>Verdadeiro caso ambas as versões sejam iguais e Falso caso contrário.</returns>
        public static bool operator ==(KardinalVersion first, string second) => first.Equals(second);

        /// <summary>
        /// Comparação de diferença entre duas versões.
        /// </summary>
        /// <param name="first">Primeira versão à ser comparada.</param>
        /// <param name="second">Segunda versão à ser comparada.</param>
        /// <returns>Verdadeiro caso ambas as versões sejam diferentes e Falso caso contrário.</returns>
        public static bool operator !=(KardinalVersion first, string second) => !first.Equals(second);

        /// <summary>
        /// Comparação de maioridade entre duas versões.
        /// </summary>
        /// <param name="first">Primeira versão à ser comparada.</param>
        /// <param name="second">Segunda versão à ser comparada.</param>
        /// <returns>Verdadeiro caso a primeira versão seja maior que a segunda e Falso caso contrário.</returns>
        public static bool operator >(KardinalVersion first, string second) => first.Newer(second);

        /// <summary>
        /// Comparação de minoridade entre duas versões.
        /// </summary>
        /// <param name="first">Primeira versão à ser comparada.</param>
        /// <param name="second">Segunda versão à ser comparada.</param>
        /// <returns>Verdadeiro caso a primeira versão seja menor que a segunda e Falso caso contrário.</returns>
        public static bool operator <(KardinalVersion first, string second) => first.Older(second);

        /// <summary>
        /// Comparação de maioridade ou igualdade entre duas versões.
        /// </summary>
        /// <param name="first">Primeira versão à ser comparada.</param>
        /// <param name="second">Segunda versão à ser comparada.</param>
        /// <returns>Verdadeiro caso a primeira versão seja maior ou igual que a segunda e Falso caso contrário.</returns>
        public static bool operator >=(KardinalVersion first, string second) => first.Equals(second) || first.Newer(second);

        /// <summary>
        /// Comparação de minoridade ou igualdade entre duas versões.
        /// </summary>
        /// <param name="first">Primeira versão à ser comparada.</param>
        /// <param name="second">Segunda versão à ser comparada.</param>
        /// <returns>Verdadeiro caso a primeira versão seja menor ou igual que a segunda e Falso caso contrário.</returns>
        public static bool operator <=(KardinalVersion first, string second) => first.Equals(second) || first.Older(second);

        /// <summary>
        /// Método estático para uma versão vazia.
        /// </summary>
        public static KardinalVersion Empty => new();

        /// <summary>
        /// Método para transformar um <see cref="Version"/> válida em <see cref="KardinalVersion"/>.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static KardinalVersion Parse(Version version)
        {
            return Parse(version.ToString());
        }

        /// <summary>
        /// Método para transformar uma string válida em <see cref="KardinalVersion"/>.
        /// </summary>
        /// <param name="major">Versão principal</param>
        /// <param name="minor">Versão secundária</param>
        /// <param name="update">Versão de atualização</param>
        /// <param name="revision">Versão de revisão</param>
        /// <returns>Instância de <see cref="KardinalVersion"/></returns>
        public static KardinalVersion Parse(int major, int minor, int update = 0, int revision = 0)
        {
            var version = $"{major}.{minor}.{update}.{revision}";
            return Parse(version);
        }

        /// <summary>
        /// Método para transformar uma string válida em <see cref="KardinalVersion"/>.
        /// </summary>
        /// <param name="version">Valor string da versão</param>
        /// <returns>Instância de <see cref="KardinalVersion"/></returns>
        public static KardinalVersion Parse(string version)
        {
            var regex = new Regex(@"\d+(\.\d+)+");
            if (!regex.IsMatch(version))
            {
                throw new InvalidVersionException(Resource.ERROR_WRONG_VERSION_FORMAT.SetParameters("VERSION", version));
            }

            var values = version.Split('.');
            KardinalVersion result;
            try
            {
                result = values.Length switch
                {
                    4 => new KardinalVersion(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3])),
                    3 => new KardinalVersion(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2])),
                    2 => new KardinalVersion(int.Parse(values[0]), int.Parse(values[1])),
                    _ => new KardinalVersion(int.Parse(values[0])),
                };
            }
            catch (Exception ex)
            {
                throw new KardinalException(ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Método que faz a tentativa de conversão de uma string em uma versão.
        /// </summary>
        /// <param name="version">Versão à ser convertida.</param>
        /// <param name="result">Resultado da conversão.</param>
        /// <returns>Verdadeiro caso a versão seja convertida com sucesso e Falso caso contrário.</returns>
        public static bool TryParse(string version, out KardinalVersion result)
        {
            try
            {
                result = Parse(version);
                return true;
            }
            catch
            {
                result = Empty;
                return false;
            }
        }

        /// <summary>
        /// Método que faz a tentativa de conversão de um <see cref="Version"/> em uma versão.
        /// </summary>
        /// <param name="version">Versão à ser convertida.</param>
        /// <param name="result">Resultado da conversão.</param>
        /// <returns>Verdadeiro caso a versão seja convertida com sucesso e Falso caso contrário.</returns>
        public static bool TryParse(Version version, out KardinalVersion result)
        {
            try
            {
                result = Parse(version);
                return true;
            }
            catch
            {
                result = Empty;
                return false;
            }
        }

        /// <summary>
        /// Método para comparar um objeto à esta versão.
        /// </summary>
        /// <param name="obj">Objeto à ser comparado</param>
        /// <returns>Valor inteiro de comparação entre objetos.</returns>
        public int CompareTo(object obj)
        {
            return obj is KardinalVersion ? CompareTo((KardinalVersion)obj) : throw new Exception();
        }

        /// <summary>
        /// Método para comparar uma versão à esta.
        /// </summary>
        /// <param name="other">Objeto à ser comparado</param>
        /// <returns></returns>
        public int CompareTo(KardinalVersion other)
        {
            var result = Compare(other);
            return result switch
            {
                VersionComparation.Current => 0,
                VersionComparation.Newer => 1,
                VersionComparation.Older => -1,
                _ => -1,
            };
        }

        /// <summary>
        /// Método para comparar uma string à esta versão.
        /// </summary>
        /// <param name="version">Valor string da versão</param>
        /// <returns></returns>
        public int CompareTo(string version)
        {
            var other = Parse(version);
            return CompareTo(other);
        }

        /// <summary>
        /// Método para verfificar se a versão informada é igual à esta.
        /// </summary>
        /// <param name="other">Versão à ser comparada</param>
        /// <returns>Verdadeiro caso as versões sejam iguais ou falso caso contrário</returns>
        public bool Equals(KardinalVersion other)
        {
            return this.Compare(other) == VersionComparation.Current;
        }

        /// <summary>
        /// Método para verfificar se a versão informada é igual à esta.
        /// </summary>
        /// <param name="version">Versão à ser comparada</param>
        /// <returns>Verdadeiro caso as versões sejam iguais ou falso caso contrário</returns>
        public bool Equals(string version)
        {
            var other = Parse(version);
            return Equals(other);
        }

        /// <summary>
        /// Método para verfificar se a versão informada é mais recente à esta.
        /// </summary>
        /// <param name="other">Versão à ser comparada</param>
        /// <returns>Verdadeiro caso a versão informada seja mais recente ou falso caso contrário</returns>
        public bool Newer(KardinalVersion other)
        {
            return this.Compare(other) == VersionComparation.Newer;
        }

        /// <summary>
        /// Método para verfificar se a versão informada é mais recente à esta.
        /// </summary>
        /// <param name="version">Versão à ser comparada</param>
        /// <returns>Verdadeiro caso a versão informada seja mais recente ou falso caso contrário</returns>
        public bool Newer(string version)
        {
            var other = Parse(version);
            return this.Compare(other) == VersionComparation.Newer;
        }

        /// <summary>
        /// Método para verfificar se a versão informada é anterior à esta.
        /// </summary>
        /// <param name="other">Versão à ser comparada</param>
        /// <returns>Verdadeiro caso a versão informada seja mais antiga ou falso caso contrário</returns>
        public bool Older(KardinalVersion other)
        {
            return this.Compare(other) == VersionComparation.Older;
        }

        /// <summary>
        /// Método para verfificar se a versão informada é anterior à esta.
        /// </summary>
        /// <param name="version">Versão à ser comparada</param>
        /// <returns>Verdadeiro caso a versão informada seja mais antiga ou falso caso contrário</returns>
        public bool Older(string version)
        {
            var other = Parse(version);
            return this.Compare(other) == VersionComparation.Older;
        }

        /// <summary>
        /// Método que faz a comparação entre versõoes.
        /// </summary>
        /// <param name="other">Versão à ser comparada com esta.</param>
        /// <returns>Enumerador representando o resultado da comparação entre as versões.</returns>
        public VersionComparation Compare(KardinalVersion other)
        {
            if (this.Major == other.Major && this.Minor == other.Minor && this.Update == other.Update && this.Revision == other.Revision)
            {
                return VersionComparation.Current;
            }

            if (this.Major > other.Major)
            {
                return VersionComparation.Newer;
            }

            if (this.Major < other.Major)
            {
                return VersionComparation.Older;
            }

            if (this.Minor > other.Minor)
            {
                return VersionComparation.Newer;
            }

            if (this.Minor < other.Minor)
            {
                return VersionComparation.Older;
            }

            if (this.Update > other.Update)
            {
                return VersionComparation.Newer;
            }

            if (this.Update < other.Update)
            {
                return VersionComparation.Older;
            }

            if (this.Revision > other.Revision)
            {
                return VersionComparation.Newer;
            }

            if (this.Revision < other.Revision)
            {
                return VersionComparation.Older;
            }

            return VersionComparation.Unknow;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj != null && obj is KardinalVersion ? Compare((KardinalVersion)obj) == VersionComparation.Current : false;
        }

        /// <summary>
        /// Método que calcula o hashCode desta instância.
        /// </summary>
        /// <returns>HashCode da instância dessa classe.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Major, this.Minor, this.Update, this.Revision);
        }

        /// <summary>
        /// Método que retorna a representação string desta instância.
        /// </summary>
        /// <returns>Representação string da instância desta classe.</returns>
        public override string ToString()
        {
            return this.Major + "." + this.Minor + ((this.Update > 0 || this.Revision > 0) ? "." + this.Update : string.Empty) + (this.Revision > 0 ? "." + this.Revision : string.Empty);
        }

        /// <summary>
        /// Método que retorna a representação string desta instância.
        /// </summary>
        /// <param name="showComplete">Indica se deve ser exibida a versão no formato completo.</param>
        /// <returns>Representação string da instância desta classe.</returns>
        public string ToString(bool showComplete)
        {
            return showComplete ? $"{this.Major}.{this.Minor}.{this.Update}].{this.Revision}" : this.ToString();
        }
    }
}
