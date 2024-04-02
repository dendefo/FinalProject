using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand(string name, string description, string prompt) : base(name, description, prompt)
        {
        }
        public override void Activate(params string[] parameters)
        {
            CommandSystem.Instance.Commands.ForEach(command => Console.WriteLine($"{command.Name} : {command.Description}"));
        }
    }
}
