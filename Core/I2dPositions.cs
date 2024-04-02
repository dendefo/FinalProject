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
            return $"x = {x}, y = {y}";
        }
        public static Position2D operator +(Position2D a, Position2D b)
        {
            return new Position2D(a.x + b.x, a.y + b.y);
        }
        public static Position2D operator -(Position2D a, Position2D b)
        {
            return new Position2D(a.x - b.x, a.y - b.y);
        }
        public static Position2D operator *(Position2D a,int b)
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
    }
    internal interface I2dPositions
    {
        //צריך להחזיק position2D
    }
}
