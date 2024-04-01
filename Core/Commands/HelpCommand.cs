using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class HelpCommand<T> : Command<T>
    {
        public HelpCommand(string name, string description, T prompt) : base(name, description, prompt)
        {
        }
        public override void Activate()
        {
            base.Activate();
            CommandSystem.Commands.ForEach(command => Console.WriteLine($"{command.Name} : {command.Description}"));
        }
    }
}
