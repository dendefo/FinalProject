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
            Engine engine = Start(8, 8);
            Scene scene = CurrentScene;
            // Load the assets
            var RookPrefab = AssetManager.LoadAsset<Rook>("Rook");
            var PawnPrefab = AssetManager.LoadAsset<Pawn>("Pawn");

            // Example of saving assets
            //AssetManager.SaveAsset(RookPrefab, "Rook");
            //AssetManager.SaveAsset(PawnPrefab, "Pawn");

            //Example of Adding assets to scene by reference to prefab component
            Instantiate(RookPrefab).TileObject.Position = new Vector2(0, 0);
            Instantiate(RookPrefab).TileObject.Position = new Vector2(7, 0);


            for (int i = 0; i < 8; i++)
            {
                Instantiate<Pawn>(PawnPrefab).TileObject.Position = new Vector2(i, 1);
            }
            for (int i = 0; i < 8; i++)
            {
                var pawn = Instantiate<Pawn>(PawnPrefab);
                pawn.TileObject.Position = new Vector2(i, 6);
                pawn.TileObject.AddComponent<CharacterRenderer>().Visuals = new('P', Color.Red);
            }
            //Example of Adding assets to scene by creating new instance
            var newrook = Instantiate(new Vector2(0, 7));
            //Add the component to the object
            newrook.AddComponent<Rook>();
            //Add the renderer to the object
            newrook.AddComponent<CharacterRenderer>().Visuals = new('R', Color.Red);

            //Example of Adding assets to scene by reference to prefab
            Instantiate(newrook).Position = new Vector2(7, 7);

            //Just to show the board rn
            engine.StartGame();
        }
    }
}