using QuarterEngine.Core;
using System.Drawing;

namespace QuarterEngine.ChessDemo.Pieces
{
    internal abstract class ChessObject<T> : TileObject<T>
    {
        public ConsoleColor ObjectColor { get; protected set; }
        public ChessObject(int PlayerId)
        {
            ObjectColor = PlayerId == 0 ? ConsoleColor.Black : ConsoleColor.DarkGray;
        }
    }
}
