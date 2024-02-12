namespace FinalProject.ChessDemo.Pieces
{
    internal class Pawn : ChessObject
    {
        public Pawn(int PlayerId):base(PlayerId)
        {
            Symbol = 'P';
        }
    }
}
