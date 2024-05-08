global using System.Drawing;
using ChessDemo.Pieces;
using Core;
using Core.Components;

namespace ChessDemo
{
    using ChessDemo.Pieces;
    using static Core.Engine;
    using System.Numerics;
    using Core.Components;
    using Renderer;
    using Core.AssetManagement;
    using Core.Commands;
    using Core.Actors;
    using System.Diagnostics;
    using Core;

    public class Programm
    {

        static void Main(string[] args)
        {
            SetUp(8, 8, new ConsoleRenderer());
            CurrentScene.ChessFloor();
            DefinePlayers(new ChessPlayerActor() { Name = "Blue", Color = Color.Blue, WinningDirection = -1 }, new StockFish() { Name = "Red", Color = Color.Red, WinningDirection = 1 });
            // Example of saving assets
            //AssetManager.SaveAsset(RookPrefab, "Rook");
            //AssetManager.SaveAsset(PawnPrefab, "Pawn");

            // Load the assets
            var RookPrefab = AssetManager.LoadAsset<Rook>("Rook");
            var PawnPrefab = AssetManager.LoadAsset<Pawn>("Pawn");
            var QueenPrefab = AssetManager.LoadAsset<Queen>("Queen");
            var BishopPrefab = AssetManager.LoadAsset<Bishop>("Bishop");
            var KnightPrefab = AssetManager.LoadAsset<Knight>("Knight");
            var KingPrefab = AssetManager.LoadAsset<King>("King");


            //Example of Adding assets to scene by reference to prefab component
            Instantiate(RookPrefab, new(0, 0), Controllers[1]);
            Instantiate(RookPrefab, new(7, 0), Controllers[1]);
            Instantiate(QueenPrefab, new(3, 0), Controllers[1]);
            Instantiate(BishopPrefab, new(2, 0), Controllers[1]);
            Instantiate(BishopPrefab, new(5, 0), Controllers[1]);
            Instantiate(KnightPrefab, new(1, 0), Controllers[1]);
            Instantiate(KnightPrefab, new(6, 0), Controllers[1]);
            Instantiate(KingPrefab, new(4, 0), Controllers[1]);
            for (int i = 0; i < 8; i++)
            {
                Instantiate(PawnPrefab, new(i, 1), Controllers[1]);
            }
            Instantiate(RookPrefab, new(0, 7), Controllers[0]);
            Instantiate(RookPrefab, new(7, 7), Controllers[0]);
            Instantiate(QueenPrefab, new(3, 7), Controllers[0]);
            Instantiate(BishopPrefab, new(2, 7), Controllers[0]);
            Instantiate(BishopPrefab, new(5, 7), Controllers[0]);
            Instantiate(KnightPrefab, new(1, 7), Controllers[0]);
            Instantiate(KnightPrefab, new(6, 7), Controllers[0]);
            Instantiate(KingPrefab, new(4, 7), Controllers[0]);
            for (int i = 0; i < 8; i++)
            {
                Instantiate(PawnPrefab, new(i, 6), Controllers[0]);
            }

            CommandSystem.Instance.AddCommand(new SelectCommand("Select"));
            CommandSystem.Instance.AddCommand(new DeselectCommand("Deselect"));
            CommandSystem.Instance.AddCommand(new AttackCommand("Move"));
            CommandSystem.Instance.AddCommand(new ShowCommand("Show"));
            CommandSystem.Instance.AddCommand(new SelectAndMoveCommand("StockFish"));
            CommandSystem.Instance.AddCommand(new FenCommand("FEN"));

            Command.CommandExecuted += CommandsCallback;
            
            Play();

        }
        public static void CommandsCallback(Command c)
        {
            switch (c)
            {
                case SelectCommand s:
                    break;
                case DeselectCommand d:
                    break;
                case AttackCommand a:
                    foreach (var controller in Controllers)
                    {
                        if ((controller as ChessActor).IsInCheck(CurrentScene))
                        {
                            ShowMessage(new("Player " + controller.Name + " is in Check! ", Color.Orange));
                            int countPossibleMoves = 0;
                            foreach (var tile in CurrentScene)
                            {
                                if (tile.TileObject == null) continue;
                                if (tile.TileObject.TryGetComponent<ControllerComponent>(typeof(ControllerComponent), out var contTemp) &&
                                    contTemp.ControllerID == controller.ControllerID)
                                {
                                    var movProvider = tile.TileObject.GetComponent<ChessComponent>(typeof(ChessComponent));
                                    var moves = movProvider.GetPossibleMoves(tile.Position, CurrentScene).Union(movProvider.GetPossibleDestroyMoves(tile.Position, CurrentScene));
                                    moves = movProvider.FilterMoves(moves, CurrentScene, contTemp, tile.Position);
                                    countPossibleMoves += moves.Count();
                                }
                            }
                            if (countPossibleMoves == 0)
                            {
                                ShowMessage(new("Player " + controller.Name + " is in CheckMate! ", Color.Red));
                                isRunning = false;
                            }
                        }

                    }
                    
                    break;
                default:
                    if (CommandSystem.Instance.SelectedObject != null)
                    {
                        ShowMessage(new(("Selected Piece: " +CommandSystem.Instance.SelectedObject.PositionToPrint.ToString()), Color.Green));
                    }
                    break;
            }
        }
    }
}
public static class ChessExtentionMethods
{
    public static string ToFENFromat(this Scene scene)
    {
        string output = "";
        for (int i = 0; i < scene.Height; i++)
        {
            int empty = 0;
            for (int j = 0; j < scene.Width; j++)
            {
                if (scene[j,i].TileObject != null && scene[j,i].TileObject.TryGetComponent(typeof(ChessComponent),out ChessComponent component))
                {
                    var piece = component.ToString();
                    if (scene[j, i].TileObject.TryGetComponent<ControllerComponent>(typeof(ControllerComponent), out var controller))
                    {
                        if (controller.ControllerID != 0)
                        {
                            piece = piece.ToLower();
                        }
                        else piece = piece.ToUpper();
                    }
                    if (empty > 0)
                    {
                        output += empty;
                        empty = 0;
                    }
                    output += piece;
                }
                else
                {
                    empty++;
                }
            }
            if (empty > 0)
            {
                output += empty;
                empty = 0;
            }
            output += "/";
        }
        output = output.Remove(output.Length - 1);
        output += Engine.CurrentController == 0 ? " w " : " b ";
        output += "- - 0 1";
        return output;
    }
}