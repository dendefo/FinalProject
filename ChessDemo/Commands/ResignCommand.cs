using Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDemo.Commands
{
    internal class ResignCommand : Command
    {
        public ResignCommand(string prompt) : base("Resign and end the game", prompt, false)
        {
        }
        public override void Activate(params string[] parameters)
        {
            base.Activate(parameters);
        }
    }
}
