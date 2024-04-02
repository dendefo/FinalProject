using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class MoveCommand : Command
    {
        public MoveCommand(string prompt) : base("Move", "Moves object to new position", prompt)
        {
        }
        public override void Activate(params string[] parameters)
        {

            if (CommandSystem.TryParsePosition(out Position2D position, parameters))
            {
                if (CommandSystem.Instance.SelectedObject == null)
                {
                    ShowMessage("No object selected");
                    return;
                }

                MoveObject(CommandSystem.Instance.SelectedObject, position);
                if (position == CommandSystem.Instance.SelectedObject.Position)
                {
                    ShowMessage("Object moved");
                }
                else
                {
                    ShowMessage("Object not moved");
                }
            }
        }
    }
}
