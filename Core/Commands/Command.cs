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
        public Command(string name, string description, string prompt)
        {
            Name = name;
            Description = description;
            Prompt = prompt;
        }

        abstract public void Activate(params string[] parameters);


    }
}
