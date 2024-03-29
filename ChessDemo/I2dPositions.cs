using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDemo
{
    readonly struct Position2D
    {
        readonly int x;
        readonly int y;
        readonly int index;

        /// <summary>
        /// x = X Position, y = Y Position, Index Position
        /// </summary>
        /// <param name=""></param>
        public Position2D(int x, int y, int index)
        {
            this.x = x;
            this.y = y;
            this.index = index;
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
            return $"x = {x}, y = {y}, index = {index}";
        }
        public static Position2D operator +(Position2D a, Position2D b)
        {
            return new Position2D(a.x + b.x, a.y + b.y, 0);
        }
        public static Position2D operator -(Position2D a, Position2D b)
        {
            return new Position2D(a.x - b.x, a.y - b.y, 0);
        }
    }
    internal interface I2dPositions
    {
        //צריך להחזיק position2D
    }
}
