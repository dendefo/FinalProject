
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
            engine.Instantiate(new Rook<char>(0, 'R'), new Vector2(0, 0));
            engine.Instantiate(new Rook<char>(0, 'R'), new Vector2(0, 7));
            for (int i = 0; i < 8; i++)
            {
                engine.Instantiate(new Pawn<char>(0, 'P'), new Vector2(i, 1));
            }
            for (int i = 0; i < 8; i++)
            {
                engine.Instantiate(new Pawn<char>(1, 'P'), new Vector2(i, 6));
            }
            engine.Instantiate(new Rook<char>(1, 'R'), new Vector2(7, 0));
            engine.Instantiate(new Rook<char>(1, 'R'), new Vector2(7, 7));
            foreach (var item in engine.map.Objects)
            {
                item.Color = System.Drawing.Color.White;
            }
            engine.map.SetColorMatrix(ConsoleColor.White, ConsoleColor.Gray);
            engine.EndTurn();
        }
    }
}