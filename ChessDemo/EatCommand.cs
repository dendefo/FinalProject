using Core.Commands;
using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDemo
{
    using static Core.Engine<char>;
    internal class EatCommand : Command
    {
        public EatCommand(string prompt) : base("Eat another piece", prompt, true)
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

                if (position == CommandSystem.Instance.SelectedObject.Position)
                {
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
