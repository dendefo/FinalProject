
namespace ChessDemo
{
    using ChessDemo.Pieces;
    using Core;
    using static Core.Engine;
    using System.Numerics;
    using Core.Components;

    public class Programm
    {
        static void Main(string[] args)
        {
            
            Engine engine = new Engine(8, 8);
            engine.Scene.DefaulChessFloor();
            Rook rook;
            //var rook = Instantiate<Rook>(new Rook(), new Vector2(0, 0));
            var rook2 = new CharacterRenderer();
            rook2.SetVisuals('R', System.Drawing.Color.Green);

            //rook.AddComponent<CharacterRenderer>().SetVisuals('R', System.Drawing.Color.Green);
            rook = Instantiate<Rook>(new Vector2(7, 0));
            rook.AddComponent<CharacterRenderer>().SetVisuals('R', System.Drawing.Color.Green);


            for (int i = 0; i < 8; i++)
            {
                var pawn = Instantiate<Pawn>(new Vector2(i, 1));
                pawn.AddComponent<CharacterRenderer>().SetVisuals('P', System.Drawing.Color.Green);
            }
            for (int i = 0; i < 8; i++)
            {
                var pawn = Instantiate<Pawn>(new Vector2(i, 6));
                pawn.AddComponent<CharacterRenderer>().SetVisuals('P', System.Drawing.Color.Red);
            }

            rook = Instantiate<Rook>(new Vector2(0, 7));
            rook.AddComponent<CharacterRenderer>().SetVisuals('R', System.Drawing.Color.Red);

            rook = Instantiate<Rook>(new Vector2(7, 7));
            rook.AddComponent<CharacterRenderer>().SetVisuals('R', System.Drawing.Color.Red);
            engine.EndTurn();
        }
    }
}