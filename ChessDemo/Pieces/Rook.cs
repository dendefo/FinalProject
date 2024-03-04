

namespace QuarterEngine.ChessDemo.Pieces
{
    internal class Rook<T> : ChessObject<T>
    {
        public Rook(int PlayerId,T visual) : base(PlayerId)
        {
            Visual = visual;
        }
    }
}
