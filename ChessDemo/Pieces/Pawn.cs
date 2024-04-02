using Core;
using Renderer;

namespace ChessDemo.Pieces
{
    internal class Pawn : ChessComponent
    {
        public override IEnumerable<Position2D> GetPossibleMoves<T>(Position2D selfPosition, Scene<T> currentGameState)
        {
            return new List<Position2D>();
        }

        public override string ToString()
        {
            return $"Pawn at Position {TileObject.Position}";
        }
    }
}
