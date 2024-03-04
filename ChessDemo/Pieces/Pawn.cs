using Renderer;

namespace QuarterEngine.ChessDemo.Pieces
{
    internal class Pawn<T> : ChessObject<T>
    {
        public Pawn(int PlayerId,VisualRepresentation<T> visuals):base(PlayerId,visuals)
        {
        }
    }
}
