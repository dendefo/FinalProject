global using System.Drawing;
namespace ChessDemo
{
    using ChessDemo.Pieces;
    using Core;
    using static Core.Engine;
    using System.Numerics;
    using Core.Components;
    using Renderer;

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
            Instantiate(RookPrefab).TileObject.Position = new Position2D(7, 0);


            for (int i = 0; i < 8; i++)
            {
                Instantiate(PawnPrefab).TileObject.Position = new Position2D(i, 1);
            }

            var RedPawn = Instantiate(PawnPrefab);
            RedPawn.TileObject.Position = new Position2D(0, 6);
            RedPawn.TileObject.AddComponent<CharacterRenderer>().Visuals = new('P', Color.Red);

            for (int i = 0; i < 8; i++)
            {
                Instantiate(RedPawn).TileObject.Position = new Position2D(i, 6);
            }

            //Example of Adding assets to scene by creating new instance
            var newrook = Instantiate(new Position2D(0, 7));
            //Add the component to the object
            newrook.AddComponent<Rook>();
            //Add the renderer to the object
            newrook.AddComponent<CharacterRenderer>().Visuals = new('R', Color.Red);

            //Example of Adding assets to scene by reference to prefab
            Instantiate(newrook, new Position2D(7, 7));
            Instantiate(RookPrefab, new Position2D(0, 0));

            //Just to show the board rn
            engine.StartGame();
        }
    }
}