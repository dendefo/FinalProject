using FinalProject.Engine;

namespace FinalProject.ChessDemo.Pieces
{
    internal abstract class ChessObject : ITileObject
    {
        public char Symbol { get; protected set; }

        public ConsoleColor ObjectColor { get; protected set; }
        public ChessObject(int PlayerId)
        {
            ObjectColor = PlayerId == 0 ? ConsoleColor.Black : ConsoleColor.DarkGray;
        }
    }
}
