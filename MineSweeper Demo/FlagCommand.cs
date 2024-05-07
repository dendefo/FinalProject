using Core.Commands;
using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper_Demo
{
    using static Core.Engine;
    internal class FlagCommand : Command
    {
        public FlagCommand(string prompt) : base("Flags Cell", prompt, true)
        {
        }
        public override void Activate(params string[] parameters)
        {

            if (CommandSystem.TryParsePosition(out Position2D position, parameters))
            {
                CurrentScene[position].TileObject.GetComponent<MineSweeperComponent>(typeof(MineSweeperComponent)).Flag();
                base.Activate(parameters);
            }
        }
    }
}
