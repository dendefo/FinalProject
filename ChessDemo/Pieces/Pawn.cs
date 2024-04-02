using Renderer;

namespace ChessDemo.Pieces
{
    internal class Pawn : ChessComponent
    {
        public override string ToString()
        {
            return $"Pawn at Position {TileObject.Position}";
        }
    }
}
