using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer
{
    public readonly struct Position2D
    {
        public readonly int x;
        public readonly int y;

        /// <summary>
        /// x = X Position, y = Y Position, Index Position
        /// </summary>
        /// <param name=""></param>
        public Position2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override readonly int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override readonly bool Equals([NotNullWhen(true)] object? obj)
        {
            return base.Equals(obj);
        }
        public override readonly string ToString()
        {
            switch (x)
            {
                case 0:
                    return $"A{y + 1}";
                case 1:
                    return $"B{y + 1}";
                case 2:
                    return $"C{y + 1}";
                case 3:
                    return $"D{y + 1}";
                case 4:
                    return $"E{y + 1}";
                case 5:
                    return $"F{y + 1}";
                case 6:
                    return $"G{y + 1}";
                case 7:
                    return $"H{y + 1}";
                default:
                    return $"x = {x}, y = {y}";
            }
        }
        public float Distance(Position2D other)
        {
            return (float)MathF.Sqrt(MathF.Pow(x - other.x, 2) + MathF.Pow(y - other.y, 2));
        }
        public static Position2D operator +(Position2D a, Position2D b)
        {
            return new Position2D(a.x + b.x, a.y + b.y);
        }
        public static Position2D operator -(Position2D a, Position2D b)
        {
            return new Position2D(a.x - b.x, a.y - b.y);
        }
        public static Position2D operator *(Position2D a, int b)
        {
            return new Position2D(a.x * b, a.y * b);
        }
        public static bool operator ==(Position2D a, Position2D b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(Position2D a, Position2D b)
        {
            return a.x != b.x && a.y != b.y;
        }
        public Position2D Abs()
        {
            return new Position2D(Math.Abs(x), Math.Abs(y));
        }
    }
}
