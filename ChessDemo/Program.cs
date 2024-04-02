global using System.Drawing;
namespace ChessDemo
{
    using ChessDemo.Pieces;
    using static Core.Engine<char>;
    using System.Numerics;
    using Core.Components;
    using Renderer;
    using Core.AssetManagement;
    using Core.Commands;

    public class Programm
    {
        static void Main(string[] args)
        {
            var engine = Start(8, 8, new ConsoleRenderer());
            var scene = CurrentScene;

            // Load the assets
            var RookPrefab = AssetManager.LoadAsset<Rook>("Rook");
            var PawnPrefab = AssetManager.LoadAsset<Pawn>("Pawn");

            // Example of saving assets
            //AssetManager.SaveAsset(RookPrefab, "Rook");
            //AssetManager.SaveAsset(PawnPrefab, "Pawn");

            //Example of Adding assets to scene by reference to prefab component
            Instantiate(RookPrefab, new(0, 0));
            Instantiate(RookPrefab, new(7, 0));


            for (int i = 0; i < 8; i++)
            {
                Instantiate(PawnPrefab, new(i, 1));
            }

            var RedPawn = Instantiate(PawnPrefab, new(0, 6));
            RedPawn.TileObject.AddComponent<CharacterRenderer>().Visuals = new('P', Color.Red);

            for (int i = 1; i < 8; i++)
            {
                Instantiate(RedPawn, new(i, 6));
            }

            //Example of Adding assets to scene by creating new instance
            var newrook = Instantiate(new Position2D(0, 7));
            //Add the component to the object
            newrook.AddComponent<Rook>();
            //Add the renderer to the object
            newrook.AddComponent<CharacterRenderer>().Visuals = new('R', Color.Red);

            Instantiate(newrook, new Position2D(7, 7));

            //Just to show the board rn
            engine.Render();
            CommandSystem.Instance.AddCommand(new MoveCommand("Move"));
            CommandSystem.Instance.AddCommand(new SelectCommand("Select"));
            CommandSystem.Instance.AddCommand(new DeselectCommand("Deselect"));
            while (true)
                CommandSystem.Instance.Listen(() => Console.ReadLine());
        }
    }
}