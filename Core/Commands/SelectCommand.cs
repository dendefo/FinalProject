using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Commands
{
    using static Core.Engine<char>;
    public class SelectCommand : Command
    {
        public SelectCommand(string prompt) : base("Select", "Selects object in coordinated", prompt)
        {
        }
        public override void Activate(params string[] parameters)
        {
            if (CommandSystem.TryParsePosition(out Position2D position, parameters))
            {
                var obj = CurrentScene[position].TileObject;
                if (obj == null)
                {
                    ShowMessage("No object at this position");
                    return;
                }
                CommandSystem.Instance.SelectedObject = obj;
                ShowMessage($"{obj.Position.ToString()} selected");
            }
        }
    }
}
