using Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDemo
{
    using static Core.Engine;
    internal class FenCommand : Command
    {
        public FenCommand(string prompt) : base("Shows Current FEN", prompt, false)
        {
        }
        public override void Activate(params string[] parameters)
        {

            ShowMessage(new("Current FEN: " + CurrentScene.ToFENFromat(), Color.White));
            base.Activate(parameters);
        }
    }
}
