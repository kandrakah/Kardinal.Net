using System;

namespace Kardinal.Net
{
    public struct Square : IComparable<Square>, IEquatable<Square>
    {
        public double SideSize { get; }

        public double Area => this.GetArea();

        public Square(int sideSize) : this((double)sideSize)
        {

        }

        public Square(float sideSize) : this((double)sideSize)
        {

        }

        public Square(decimal sideSize) : this((double)sideSize)
        {

        }

        public Square(double sideSize)
        {
            this.SideSize = sideSize;
        }

        public int CompareTo(Square other)
        {
            if (this.SideSize > other.SideSize || this.Area > other.Area)
            {
                return 1;
            }
            else if (this.SideSize < other.SideSize || this.SideSize < other.SideSize)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            return other is Square && this.GetHashCode() == other.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Square other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }

        /// <summary>
        /// Método que calcula o hashCode desta instância.
        /// </summary>
        /// <returns>HashCode da instância dessa classe.</returns>
        public override int GetHashCode()
        {
            var hashCode = 416361249;
            hashCode = hashCode * -1235422649 + this.SideSize.GetHashCode();
            hashCode = hashCode * -1235422649 + this.Area.GetHashCode();
            return hashCode;
        }

        private double GetArea()
        {
            return Math.Pow((double)this.SideSize, 2);
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return this.SideSize.ToString();
        }
    }
}
