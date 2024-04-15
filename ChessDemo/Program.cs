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
    using Core.Actors;

    public class Programm
    {
        static void Main(string[] args)
        {
            var engine = SetUp(8, 8, new ConsoleRenderer());
            var scene = CurrentScene;
            DefinePlayers(new ChessPlayerActor() { Name = "First", Color = Color.Blue, WinningDirection = 1 }, new ChessPlayerActor() { Name = "Second", Color = Color.Red, WinningDirection = -1 });

            // Load the assets
            var RookPrefab = AssetManager.LoadAsset<Rook>("Rook");
            var PawnPrefab = AssetManager.LoadAsset<Pawn>("Pawn");
            var QueenPrefab = AssetManager.LoadAsset<Queen>("Queen");

            // Example of saving assets
            //AssetManager.SaveAsset(RookPrefab, "Rook");
            //AssetManager.SaveAsset(PawnPrefab, "Pawn");

            //Example of Adding assets to scene by reference to prefab component
            Instantiate(RookPrefab, new(0, 0), Controllers[0]);
            Instantiate(RookPrefab, new(7, 0), Controllers[0]);
            Instantiate(QueenPrefab, new(4,7), Controllers[1]);
            Instantiate(QueenPrefab, new(4,0), Controllers[0]);


            for (int i = 0; i < 8; i++)
            {
                Instantiate(PawnPrefab, new(i, 1), Controllers[0]);
            }

            var RedPawn = Instantiate(PawnPrefab, new(0, 6), Controllers[1]);
            RedPawn.TileObject.AddComponent<CharacterRenderer>().Visuals = new('P', Color.Red);

            for (int i = 1; i < 8; i++)
            {
                Instantiate(RedPawn, new(i, 6), Controllers[1]);
            }

            //Example of Adding assets to scene by creating new instance
            var newrook = Instantiate(new Position2D(0, 7), Controllers[1]);
            //Add the component to the object
            newrook.AddComponent<Rook>();
            //Add the renderer to the object
            newrook.AddComponent<CharacterRenderer>().Visuals = new('R', Color.Red);

            Instantiate(newrook, new Position2D(7, 7), Controllers[1]);

            CommandSystem.Instance.AddCommand(new MoveCommand("Move", true));
            CommandSystem.Instance.AddCommand(new SelectCommand("Select"));
            CommandSystem.Instance.AddCommand(new DeselectCommand("Deselect"));
            CommandSystem.Instance.AddCommand(new EatCommand("Eat"));
            Play();
        }
    }
}