using ChessDemo.Commands;
using ChessDemo.Pieces;
using Core;
using Core.Actors;
using Core.Commands;
using Core.Components;
using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ChessDemo
{
    using static Engine;
    internal class SelectAndMoveCommand : Command
    {
        public SelectAndMoveCommand(string prompt) : base("Selects piece and Makes turn a7a8q (last one is promotion argument)", prompt, true)
        {
        }
        public override void Activate(params string[] parameters)
        {
            string selectParams = parameters[1][..2];
            string moveParams = parameters[1][2..4];
            char promoteParam =' ';
            if (parameters[1].Length==5) promoteParam = parameters[1][4];

            if (CommandSystem.TryParsePosition(out Position2D position, Prompt, selectParams))
            {
                var obj = CurrentScene[position].TileObject;
                if (obj == null)
                {
                    ShowMessage(new("No object at this position", Color.Orange));
                    return;
                }
                var comp = obj.GetComponent<ControllerComponent>(typeof(ControllerComponent));
                if (comp == null) { ShowMessage(new("This Object can't be controlled", Color.Orange)); return; }
                if (comp.ControllerID != CurrentController)
                {
                    ShowMessage(new("This Object can't be controlled by you", Color.Orange));
                    return;
                }
                CommandSystem.Instance.SelectedObject = obj;
                ShowMessage(new($"{obj.PositionToPrint} selected", Color.Green));
                //ShowMoves(obj);

            }

            if (CommandSystem.TryParsePosition(out position, Prompt, moveParams))
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
                //VERY DIRTY
                //Promotion Rule, this is intended for the AI to fill, but player can also use it
                var controller = Controllers[CurrentController] as ChessActor;
                if (CommandSystem.Instance.SelectedObject.TryGetComponent<Pawn>(typeof(Pawn),out var pawn))
                {
                    if (position.y == 0 && controller.WinningDirection == -1 || position.y == CurrentScene.Height - 1 && controller.WinningDirection == 1)
                    {
                        switch (promoteParam)
                        {
                            case 'q' or 'Q':
                                pawn.Promote<Queen>("Queen");
                                break;
                            case 'r' or 'R':
                                pawn.Promote<Rook>("Rook");
                                break;
                            case 'n' or 'N':
                                pawn.Promote<Knight>("Knight");
                                break;
                            case 'b' or 'B':
                                pawn.Promote<Bishop>("Bishop");
                                break;
                            default:
                                PromoteCommand.ChoosePromotion();
                                break;
                        }
                    }
                }
                if (position == CommandSystem.Instance.SelectedObject.Position)
                {
                    //Object moved
                    ShowMessage(new("Object moved", Color.Green));
                    base.Activate(Prompt, moveParams);
                }
                else
                {
                    ShowMessage(new("Object not moved", Color.Orange));
                }
            }
        }
    }
}
