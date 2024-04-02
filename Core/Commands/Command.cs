using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Commands
{
    abstract public class Command
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Prompt;
        public bool DoesEndTurn { get; protected set; } = false;
        public static event Action<Command> CommandExecuted;
        public Command(string description, string prompt, bool doesEndTurn)
        {
            Name = prompt;
            Description = description;
            Prompt = prompt;
            DoesEndTurn = doesEndTurn;
        }

        virtual public void Activate(params string[] parameters) => CommandExecuted?.Invoke(this);


    }
}
