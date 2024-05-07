﻿namespace MineSweeper_Demo
{
    using Core;
    using Core.Commands;
    using Core.Components;
    using Renderer;
    using static Core.Engine;
    internal class Program
    {
        static void Main(string[] args)
        {
            SetUp(9, 9, new ConsoleRenderer());
            DefinePlayers(new MineSweeperActor());

            foreach (var tile in CurrentScene)
            {
                tile.TileObject = Instantiate(tile.Position);
                var renderer = tile.TileObject.AddComponent<CharacterRenderer>();
                tile.TileObject.AddComponent<MineSweeperComponent>().Renderer = renderer;
                tile.TileObject.AddComponent<PlayerComponent>().ControllerID = 0;

            }

            int mines = 10;
            while (mines > 0)
            {
                var tile = CurrentScene[Random.Shared.Next(0, CurrentScene.Width), Random.Shared.Next(0, CurrentScene.Height)];
                if (tile.TileObject.TryGetComponent<MineSweeperComponent>(typeof(MineSweeperComponent), out var comp))
                {
                    comp.IsMine = true;
                    mines--;
                }
            }
            MineSweeperComponent.OnMineHit += Lose;
            MineSweeperComponent.OnWin += Win;
            CommandSystem.Instance.AddCommand(new SelectCommand("Select"));
            CommandSystem.Instance.AddCommand(new FlagCommand("Flag"));
            Command.CommandExecuted += CommandListener;
            Play();

        }

        private static void Win()
        {
            isRunning = false;
            Console.WriteLine("You Won!");
        }

        private static void Lose()
        {
            isRunning = false;
            Console.WriteLine("You Lost!");
        }

        private static void CommandListener(Command command)
        {
            switch (command)
            {
                case SelectCommand select:
                    if (CommandSystem.Instance.SelectedObject == null) break;
                    var temp = CommandSystem.Instance.SelectedObject;
                    if (temp.TryGetComponent<MineSweeperComponent>(typeof(MineSweeperComponent), out var comp))
                    {
                        comp.Reveal();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}