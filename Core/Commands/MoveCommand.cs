using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class MoveCommand<T> : Command<T>
    {
        public MoveCommand(T prompt):base("Move", "Moves object to new position",prompt)
        {
        }
        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("Move Command Activated");
        }
    }
}
