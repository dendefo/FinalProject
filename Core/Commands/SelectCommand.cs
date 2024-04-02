using Core.Actors;
using Core.Components;
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
        public SelectCommand(string prompt) : base("Selects object in coordinated", prompt, false)
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
                var comp = obj.GetComponent<PlayerComponent>(typeof(PlayerComponent));
                if (comp == null) { ShowMessage("This Object can't be controlled"); return; }
                if (comp.ControllerID != CurrentController)
                {
                    ShowMessage("This Object can't be controlled by you");
                    return;
                }
                CommandSystem.Instance.SelectedObject = obj;
                ShowMessage($"{obj.Position.ToString()} selected");
                base.Activate(parameters);

            }
        }
    }
}
