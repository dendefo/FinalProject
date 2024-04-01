using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Commands
{
    abstract public class Command<T>
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public T Prompt;
        public static event Action<Command<T>> OnCommand;
        public Command(string name, string description, T prompt)
        {
            Name = name;
            Description = description;
            Prompt = prompt;
        }

        virtual public void Activate() => OnCommand?.Invoke(this);


    }
}
