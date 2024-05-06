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
            DefinePlayers(new ChessPlayerActor() { Name = "White", Color = Color.Red, WinningDirection = -1 }, new ChessPlayerActor() { Name = "Black", Color = Color.Blue, WinningDirection = 1 });

            // Load the assets
            var RookPrefab = AssetManager.LoadAsset<Rook>("Rook");
            var PawnPrefab = AssetManager.LoadAsset<Pawn>("Pawn");
            var QueenPrefab = AssetManager.LoadAsset<Queen>("Queen");
            var BishopPrefab = AssetManager.LoadAsset<Bishop>("Bishop");
            var KnightPrefab = AssetManager.LoadAsset<Knight>("Knight");
            var KingPrefab = AssetManager.LoadAsset<King>("King");
            // Example of saving assets
            //AssetManager.SaveAsset(RookPrefab, "Rook");
            //AssetManager.SaveAsset(PawnPrefab, "Pawn");


            //Example of Adding assets to scene by reference to prefab component
            Instantiate(RookPrefab, new(0, 0), Controllers[0]);
            Instantiate(RookPrefab, new(7, 0), Controllers[0]);
            Instantiate(QueenPrefab, new(3, 0), Controllers[0]);
            Instantiate(BishopPrefab, new(2, 0), Controllers[0]);
            Instantiate(BishopPrefab, new(5, 0), Controllers[0]);
            Instantiate(KnightPrefab, new(1, 0), Controllers[0]);
            Instantiate(KnightPrefab, new(6, 0), Controllers[0]);
            Instantiate(KingPrefab, new(4, 0), Controllers[0]);
            for (int i = 0; i < 8; i++)
            {
                Instantiate(PawnPrefab, new(i, 1), Controllers[0]);
            }
            Instantiate(RookPrefab, new(0, 7), Controllers[1]);
            Instantiate(RookPrefab, new(7, 7), Controllers[1]);
            Instantiate(QueenPrefab, new(3, 7), Controllers[1]);
            Instantiate(BishopPrefab, new(2, 7), Controllers[1]);
            Instantiate(BishopPrefab, new(5, 7), Controllers[1]);
            Instantiate(KnightPrefab, new(1, 7), Controllers[1]);
            Instantiate(KnightPrefab, new(6, 7), Controllers[1]);
            Instantiate(KingPrefab, new(4, 7), Controllers[1]);
            for (int i = 0; i < 8; i++)
            {
                Instantiate(PawnPrefab, new(i, 6), Controllers[1]);
            }

            CommandSystem.Instance.AddCommand(new SelectCommand("Select"));
            CommandSystem.Instance.AddCommand(new DeselectCommand("Deselect"));
            CommandSystem.Instance.AddCommand(new AttackCommand("Move"));
            Play();
        }
    }
}