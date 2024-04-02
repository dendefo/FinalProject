using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand(string description, string prompt) : base( description, prompt, false)
        {
        }
        public override void Activate(params string[] parameters)
        {
            CommandSystem.Instance.Commands.ForEach(command => ShowMessage($"{command.Name} : {command.Description}"));
            base.Activate(parameters);
        }
    }
}
