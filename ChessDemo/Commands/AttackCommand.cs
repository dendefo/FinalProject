using ChessDemo.Commands;
using ChessDemo.Pieces;
using Core.Commands;
using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDemo
{
    using static Core.Engine;
    internal class AttackCommand : Command
    {
        public AttackCommand(string prompt) : base("Move Chess piece", prompt, true)
        {
        }
        public override void Activate(params string[] parameters)
        {
            if (CommandSystem.TryParsePosition(out Position2D position, parameters))
            {
                if (CommandSystem.Instance.SelectedObject == null)
                {
                    ShowMessage(new("No object selected", Color.Orange));
                    return;
                }
                if (CommandSystem.Instance.SelectedObject.Position == position)
                {
                    ShowMessage(new("Object already at this position", Color.Orange));
                    return;
                }
                MoveObject(CommandSystem.Instance.SelectedObject, position, true);
                var controller = Controllers[CurrentController] as ChessActor;

                if (CommandSystem.Instance.SelectedObject.TryGetComponent<Pawn>(typeof(Pawn), out var pawn))
                {
                    if (position.y == 0 && controller.WinningDirection == -1 || position.y == CurrentScene.Height - 1 && controller.WinningDirection == 1)
                    {
                        PromoteCommand.ChoosePromotion();
                    }
                }
                if (position == CommandSystem.Instance.SelectedObject.Position)
                {
                    //Object moved
                    ShowMessage(new("Object moved", Color.Green));
                    base.Activate(parameters);
                }
                else
                {
                    ShowMessage(new("Object not moved", Color.Orange));
                }
            }
        }
    }
}
