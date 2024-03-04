
namespace QuarterEngine.ChessDemo
{
    using QuarterEngine.ChessDemo.Pieces;
    using QuarterEngine.Core;
    using System.Numerics;

    public class Programm
    {
        static void Main(string[] args)
        {
            Core engine = new Core(8, 8);
            engine.Scene.DefaulChessFloor();
            engine.Instantiate(new Rook<char>(0, new('R', System.Drawing.Color.Green)), new Vector2(0, 0));
            engine.Instantiate(new Rook<char>(0, new('R', System.Drawing.Color.Green)), new Vector2(7, 0));
            for (int i = 0; i < 8; i++)
            {
                engine.Instantiate(new Pawn<char>(0, new('P', System.Drawing.Color.Green)), new Vector2(i, 1));
            }
            for (int i = 0; i < 8; i++)
            {
                engine.Instantiate(new Pawn<char>(1, new('P', System.Drawing.Color.Red)), new Vector2(i, 6));
            }
            engine.Instantiate(new Rook<char>(1, new('R', System.Drawing.Color.Red)), new Vector2(0, 7));
            engine.Instantiate(new Rook<char>(1, new('R', System.Drawing.Color.Red)), new Vector2(7, 7));
            engine.EndTurn();
        }
    }
}