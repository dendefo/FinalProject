global using System.Drawing;
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
            var rook2 = new CharacterRenderer();
            rook2.SetVisuals('R', Color.Green);

            var PrefabComponent = Instantiate<Rook>(new Vector2(7, 0));
            PrefabComponent.AddComponent<CharacterRenderer>().SetVisuals('R', Color.Green);

            var Copy = Instantiate(PrefabComponent);
            Copy.TileObject.Position = new Vector2(0, 7);
            Copy.GetComponent<CharacterRenderer>().SetVisuals('R', Color.Red);

            for (int i = 0; i < 8; i++)
            {
                var pawn = Instantiate<Pawn>(new Vector2(i, 1));
                pawn.AddComponent<CharacterRenderer>().SetVisuals('P', Color.Green);
            }
            for (int i = 0; i < 8; i++)
            {
                var pawn = Instantiate<Pawn>(new Vector2(i, 6));
                pawn.AddComponent<CharacterRenderer>().SetVisuals('P', Color.Red);
            }


            var prefabCopy = Instantiate(PrefabComponent.TileObject);
            prefabCopy.AddComponent<CharacterRenderer>().SetVisuals('R', Color.Red);
            prefabCopy.Position = new Vector2(7, 7);
            engine.EndTurn();
        }
    }
}