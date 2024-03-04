namespace QuarterEngine.ChessDemo.Pieces
{
    internal class Pawn<T> : ChessObject<T>
    {
        public Pawn(int PlayerId,T visual):base(PlayerId)
        {
            Visual = visual;
        }
    }
}
