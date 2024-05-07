using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    using System.Drawing;
    public class MoveCommand : Command
    {
        public MoveCommand(string prompt, bool doesEndTurn) : base("Moves object to new position", prompt, doesEndTurn)
        {
        }
        public override void Activate(params string[] parameters)
        {

            if (CommandSystem.TryParsePosition(out Position2D position, parameters))
            {
                if (CommandSystem.Instance.SelectedObject == null)
                {
                    ShowMessage(new("No object selected",Color.Orange));
                    return;
                }
                if (CommandSystem.Instance.SelectedObject.Position == position)
                {
                    ShowMessage(new("Object already at this position", Color.Orange));
                    return;
                }
                MoveObject(CommandSystem.Instance.SelectedObject, position);
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
