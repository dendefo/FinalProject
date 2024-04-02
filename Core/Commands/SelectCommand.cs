﻿using Core.Actors;
using Core.Components;
using Renderer;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                    ShowMessage(new("No object at this position", Color.Orange));
                    return;
                }
                var comp = obj.GetComponent<PlayerComponent>(typeof(PlayerComponent));
                if (comp == null) { ShowMessage(new("This Object can't be controlled", Color.Orange)); return; }
                if (comp.ControllerID != CurrentController)
                {
                    ShowMessage(new("This Object can't be controlled by you", Color.Orange));
                    return;
                }
                CommandSystem.Instance.SelectedObject = obj;
                ShowMessage(new($"{obj.Position} selected", Color.Green));
                base.Activate(parameters);

            }
        }
    }
}
