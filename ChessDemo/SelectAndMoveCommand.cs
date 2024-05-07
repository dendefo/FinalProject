using Core;
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
        public SelectAndMoveCommand(string prompt) : base("Selects piece and Makes turn", prompt, true)
        {
        }
        public override void Activate(params string[] parameters)
        {
            string selectParams = parameters[1][..2];
            string moveParams = parameters[1][2..];

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
