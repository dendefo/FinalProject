global using System.Drawing;
using ChessDemo.Pieces;
using Core;
using Core.Components;
using Core.Rendering;

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
    using ChessDemo.Commands;

    public class Programm
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Chess Game!");
            Console.WriteLine("Choose Difficulty from 1 to 15");
            uint difficulty;
            while (!uint.TryParse(Console.ReadLine(), out difficulty)) ;
            SetUp(8, 8, new ConsoleRenderer());
            CurrentScene.ChessFloor();
            DefinePlayers(new ChessPlayerActor() { Name = "Blue", Color = Color.Blue, WinningDirection = -1 }, new ChessPlayerActor() { Name = "Red", Color = Color.Red, WinningDirection = 1});
            // Example of saving assets
            //AssetManager.SaveAsset(RookPrefab, "Rook");
            //AssetManager.SaveAsset(PawnPrefab, "Pawn");

            SetUpPiecesForTwoPlayers();

            CommandSystem.Instance.AddCommand(new SelectCommand("Select"));
            CommandSystem.Instance.AddCommand(new DeselectCommand("Deselect"));
            CommandSystem.Instance.AddCommand(new AttackCommand("Move"));
            CommandSystem.Instance.AddCommand(new ShowCommand("Show"));
            CommandSystem.Instance.AddCommand(new SelectAndMoveCommand("StockFish"));
            CommandSystem.Instance.AddCommand(new FenCommand("FEN"));
            CommandSystem.Instance.AddCommand(new ResignCommand("Resign"));

            Command.CommandExecuted += CommandsCallback;

            Play();
            Command.CommandExecuted -= CommandsCallback;
            Thread.Sleep(10000);

        }
        public static void SetUpPiecesForTwoPlayers()
        {
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
        }
        public static void CommandsCallback(Command c)
        {
            switch (c)
            {
                case ResignCommand r:
                    ShowMessage(new("Player " + Controllers[CurrentController].Name + " has resigned! ", Color.Red));
                    Stop();
                    break;
                case AttackCommand a:
                case SelectAndMoveCommand m:
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
                                ShowMessage(new("Player " + controller.Name + " is in Mate! ", Color.Red));
                                Stop();
                            }
                        }

                    }

                    break;
                default:
                    if (CommandSystem.Instance.SelectedObject != null)
                    {
                        ShowMessage(new(("Selected Piece: " + CommandSystem.Instance.SelectedObject.PositionToPrint.ToString()), Color.Green));
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
        int KingSideWhiteCastle = 0;
        int QueenSideWhiteCastle = 0;
        int KingSideBlackCastle = 0;
        int QueenSideBlackCastle = 0;
        string output = "";
        for (int i = 0; i < scene.Height; i++)
        {
            int empty = 0;
            for (int j = 0; j < scene.Width; j++)
            {
                if (scene[j, i].TileObject != null && scene[j, i].TileObject.TryGetComponent(typeof(ChessComponent), out ChessComponent component))
                {
                    var piece = component.ToString();
                    if (scene[j, i].TileObject.TryGetComponent<ControllerComponent>(typeof(ControllerComponent), out var controller))
                    {
                        if (controller.ControllerID != 0)
                        {
                            if (component is King king && king.isFirstMove)
                            {
                                KingSideBlackCastle++;
                                QueenSideBlackCastle++;
                            }
                            else if (component is Rook rook && rook.isFirstMove)
                            {
                                if (rook.Position.x == 0) QueenSideBlackCastle++;
                                else if (rook.Position.x == 7) KingSideBlackCastle++;
                            }
                            piece = piece.ToLower();
                        }
                        else
                        {
                            if (component is King king && king.isFirstMove)
                            {
                                KingSideWhiteCastle++;
                                QueenSideWhiteCastle++;
                            }
                            else if (component is Rook rook && rook.isFirstMove)
                            {
                                if (rook.Position.x == 0) QueenSideWhiteCastle++;
                                else if (rook.Position.x == 7) KingSideWhiteCastle++;
                            }
                            piece = piece.ToUpper();
                        }
                    }
                    if (empty > 0)
                    {
                        output += empty;
                        empty = 0;
                    }
                    output += piece;
                }
                else empty++;
            }
            if (empty > 0) output += empty;
            output += "/";
        }
        output = output.Remove(output.Length - 1);
        output += Engine.CurrentController == 0 ? " w " : " b ";
        var castlings = (KingSideWhiteCastle == 2 ? "K" : "") + (QueenSideWhiteCastle == 2 ? "Q" : "") + (KingSideBlackCastle == 2 ? "k" : "") + (QueenSideBlackCastle == 2 ? "q" : "");
        output += castlings == "" ? "-" : castlings;
        output += " ";
        if (Pawn.CurrentEnPassaunt != default)
        {
            var enPassaunt = Pawn.CurrentEnPassaunt;
            output += (IRenderer.ConvertIntToString(enPassaunt.x + 1) + (8 - enPassaunt.y)).ToLower();
        }
        else output += "-";
        output += " ";
        //Implemet Halfmove and Fullmove counter and also Draw Condition!
        output += "0 1";
        return output;
    }
}