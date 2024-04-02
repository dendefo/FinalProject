﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class MoveCommand : Command
    {
        public MoveCommand(string prompt):base("Move", "Moves object to new position",prompt)
        {
        }
        public override void Activate(params string[] parameters)
        {

            Console.WriteLine("Move Command Activated");
        }
    }
}
