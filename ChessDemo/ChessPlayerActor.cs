using Core.Actors;
using Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDemo
{
    internal class ChessPlayerActor : ChessActor
    {
        override public void StartTurn()
        {
            CommandSystem.Instance.Listen(() => Console.ReadLine());
        }
    }
}
