using ChessDemo.Pieces;
using Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDemo.Commands
{
    using static Core.Engine;
    internal class PromoteCommand : Command
    {
        public PromoteCommand(string prompt) : 
            base("promote to a new piece: Q-Queen, R-Rook, N-Knight, B-Bishop", prompt, false)
        {
        }
        public override void Activate(params string[] parameters)
        {
            if (parameters.Length == 1)
            {
                ShowMessage(new("You must specify a piece to promote to", Color.Orange));
                return;
            }
            if (parameters[1].Length != 1)
            {
                ShowMessage(new("You must specify a single character", Color.Orange));
                return;
            }
            char piece = parameters[1].ToUpper()[0];
            switch (piece)
            {
                case 'Q':
                    CommandSystem.Instance.SelectedObject.GetComponent<Pawn>(typeof(Pawn)).Promote<Queen>("Queen");
                    break;
                case 'R':
                    CommandSystem.Instance.SelectedObject.GetComponent<Pawn>(typeof(Pawn)).Promote<Rook>("Rook");
                    break;
                case 'N':
                    CommandSystem.Instance.SelectedObject.GetComponent<Pawn>(typeof(Pawn)).Promote<Knight>("Knight");
                    break;
                case 'B':
                    CommandSystem.Instance.SelectedObject.GetComponent<Pawn>(typeof(Pawn)).Promote<Bishop>("Bishop");
                    break;
                default:
                    ShowMessage(new("Invalid piece", Color.Orange));
                    return;
                
            }
            base.Activate(parameters);
        }
        public static void ChoosePromotion()
        {
            List<Command> currentCommands = new();

            for (int i = CommandSystem.Instance.Commands.Count - 1; i >= 0; i--)
            {
                Command? command = CommandSystem.Instance.Commands[i];
                if (command is not HelpCommand)
                {
                    CommandSystem.Instance.Commands.Remove(command);
                    currentCommands.Add(command);
                }
            }
            var promotion = new PromoteCommand("Promote");
            CommandSystem.Instance.Commands.Add(promotion);
            while (CurrentScene[CommandSystem.Instance.SelectedObject.Position].TileObject.TryGetComponent<Pawn>(typeof(Pawn), out var pawn))
            {
                ShowMessage(new("Pawn promotion", Color.Green));
                Render();
                CurrentScene.ClearHighlights();
                CommandSystem.Instance.Listen(() => Console.ReadLine());
            }
            CommandSystem.Instance.Commands.Remove(promotion);
            for (int i = currentCommands.Count - 1; i >= 0; i--)
            {
                Command? command = currentCommands[i];
                CommandSystem.Instance.Commands.Add(command);
            }
        }
    }
}
